import * as React from "react"
import {
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"
import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"
import { upsertKetQua, confirmKetQua } from "@/api/api-ketqua"
import { upsertDiemDinhChinh } from "@/api/api-diemdinhchinh"
import { useToast } from "@/hooks/use-toast";
import { Dialog, DialogTrigger, DialogContent, DialogHeader, DialogTitle, DialogDescription, DialogFooter } from "@/components/ui/dialog"
import { DialogClose } from "@radix-ui/react-dialog"

export function GradeTable({
  data,
  fetchData,
  components,
  questions,
  isGiangVienMode,
  isConfirmed,
}) {
  const [tableData, setTableData] = React.useState(data)
  const [isEditing, setIsEditing] = React.useState(false)
  const [modifiedRecords, setModifiedRecords] = React.useState([])
  const [modifiedDiemDinhChinhRecords, setModifiedDiemDinhChinhRecords] = React.useState([])
  console.log("isConfirmed", isConfirmed);
  const { toast } = useToast();

  React.useEffect(() => {
    setTableData(data)
  }, [data])

  const columns = React.useMemo(() => {
    const cols = [
      {
        accessorKey: "id",
        header: "Id",
        size: 60,
      },
      // {
      //   accessorKey: "mssv",
      //   header: "MSSV",
      //   size: 120,
      // },
      {
        accessorKey: "ten",
        header: "Họ và tên",
        size: 200,
      },
    ]

    // Add columns for each component and its questions
    components.forEach((component) => {
      const componentQuestions = questions[component.id.toString()] || []
      cols.push({
        id: component.loai,
        header: `${component.loai} (${component.trongSo * 100}%)`,
        columns: [
          ...componentQuestions.map((question) => ({
            accessorFn: (row) =>
              row.grades[component.loai]?.[question.id.toString()] ?? "",
            id: `${component.loai}_${question.id}`,
            header: () => (
              <div>
                <div>{question.ten}</div>
                <div>{question.trongSo * 100}%</div>
              </div>
            ),
            size: 80,
            cell: ({ row, column }) => (
              <EditableCell
                value={row.getValue(column.id)}
                onChange={(value) => {
                  const newData = [...tableData]
                  console.log("newData", newData);
                  const rowIndex = row.index
                  const [componentId, questionId] = column.id.split("_")
                  
                  if (!newData[rowIndex].grades[componentId]) {
                    newData[rowIndex].grades[componentId] = {}
                  }
                  
                  newData[rowIndex].grades[componentId][questionId] = value
                  // const ketQuaId = newData[rowIndex].ketQuas[componentId][questionId];
                  const modifiedRecord = {
                    sinhVienId: newData[rowIndex].id,
                    cauHoiId: parseInt(questionId),
                    diemTam: value,
                  }

                  const modifiedDiemDinhChinhRecord = {
                    sinhVienId: newData[rowIndex].id,
                    cauHoiId: parseInt(questionId),
                    diemMoi: value,
                  }
                  console.log("modifiedRecord", modifiedRecord);
                  console.log("modifiedDiemDinhChinhRecord", modifiedDiemDinhChinhRecord);
                  // upsertKetQua(modifiedRecord);
                  if (modifiedRecord.diemTam !== null) {
                    setModifiedRecords([...modifiedRecords, modifiedRecord]);
                  }

                  if (modifiedDiemDinhChinhRecord.diemMoi !== null) {
                    setModifiedDiemDinhChinhRecords([...modifiedDiemDinhChinhRecords, modifiedDiemDinhChinhRecord]);
                  }
                  setTableData(newData)
                }}
                isEditing={isEditing}
              />
            ),
          })),
          {
            id: `${component.loai}_total`,
            header: "Tổng",
            size: 80,
            accessorFn: (row) => {
              const grades = row.grades[component.loai] || {}
              return Object.values(grades).reduce((sum, score) => sum + score, 0)
            },
          },
        ],
      })
    })

    return cols
  }, [components, questions, isEditing, tableData, modifiedRecords, modifiedDiemDinhChinhRecords])

  const table = useReactTable({
    data: tableData,
    columns,
    getCoreRowModel: getCoreRowModel(),
  })

  const handleSaveChanges = async () => {
    setIsEditing(false)
    for (let i = 0; i < modifiedRecords.length; i++) {
      try {
        console.log("modifiedRecords[i]", modifiedRecords[i]);
        await upsertKetQua(modifiedRecords[i]);
        fetchData();
      } catch (error) {
        console.error(`Error updating record for sinhVienId ${modifiedRecords[i].sinhVienId}:`, error);
      }
    }
  }

  const handleSaveDinhChinh = async () => {
    setIsEditing(false)
    for (let i = 0; i < modifiedDiemDinhChinhRecords.length; i++) {
      try {
        console.log("modifiedDiemDinhChinhRecords[i]", modifiedDiemDinhChinhRecords[i]);
        await upsertDiemDinhChinh(modifiedDiemDinhChinhRecords[i]);
        fetchData();
      } catch (error) {
        console.error(`Error updating Diem Dinh Chinh record for ketQuaId ${modifiedDiemDinhChinhRecords[i].ketQuaId}:`, error);
      }
    }
    toast({
      title: "Đã cập nhật điểm đính chính",
      description: "Xem điểm đính chính đã tạo ở mục Điểm Đính Chính",
      variant: "success",
    });
  }
  console.log("components, questions", components, questions);

  const handleConfirm = async () => {
    try {
      const hasNullGrades = tableData.some(record => 
        Object.values(record.grades) // Get all grade objects
          .flatMap(gradeObj => Object.values(gradeObj)) // Flatten values into single array
          .some(grade => grade === null) // Check for null
      );
      if (hasNullGrades) {
        toast({
          title: "Chưa nhập đủ điểm",
          description: "Vui lòng nhập đủ điểm cho tất cả sinh viên trước khi xác nhận",
          variant: "destructive",
        });
        return;
      }

      const confirmKetQuaDTOs = tableData.flatMap(record => 
        record.cauHois.map(cauHoiId => ({
          sinhVienId: record.id,
          cauHoiId: cauHoiId
        }))
      );

      confirmKetQuaDTOs.forEach(async (confirmKetQuaDTO) => {
        await confirmKetQua(confirmKetQuaDTO);
        console.log(confirmKetQuaDTO);
      });

    } catch (error) {
      console.error("Error confirming records:", error);
      toast({
        title: "Lỗi xác nhận điểm",
        description: "Đã có lỗi xảy ra khi xác nhận điểm",
        variant: "destructive",
      });
    }

    toast({
      title: "Đã xác nhận điểm",
      description: "Điểm đã được xác nhận và không thể chỉnh sửa",
      variant: "success",
    });

    fetchData();
  }


  // function that compare date to today and return the result
  const isDatePassed = (dateString) => {
    const today = new Date();
    const date = new Date(dateString);
    return date.getTime() < today.getTime();
  }

  const canEditDiem = !isGiangVienMode || (isGiangVienMode && isDatePassed(components[0].ngayMoNhapDiem) && !isDatePassed(components[0].hanNhapDiem));
  const canDinhChinhDiem = isGiangVienMode && !isDatePassed(components[0].hanDinhChinh) && isConfirmed;

  return (
    <div className="space-y-4">
      <div className="flex justify-end gap-1">
      <Button
        disabled={isConfirmed || !canEditDiem}
        onClick={() => isEditing ? handleSaveChanges() : setIsEditing(true)}
      >
        {isEditing ? "Lưu" : "Sửa Điểm"}
      </Button>
      <Dialog>
        <DialogTrigger asChild>
        {isGiangVienMode && (
          <Button
            disabled={isConfirmed}
            // onClick={handleConfirm}
          >
            {isConfirmed ? "Đã Xác Nhận" : "Xác Nhận"}
          </Button>
        )}
        </DialogTrigger>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>Xác nhận điểm</DialogTitle>
            <DialogDescription>
              Xác nhận điểm của sinh viên
            </DialogDescription>
          </DialogHeader>
            <p>Điểm đã xác nhận thì không thể chỉnh sửa, bạn có muốn xác nhận?</p>
          <DialogFooter>
            <DialogClose asChild>
              <Button type="submit" onClick={() => handleConfirm()}>
                Xác nhận
              </Button>
            </DialogClose>
          </DialogFooter>
        </DialogContent>
      </Dialog>
      {isGiangVienMode && (
        <Button
          disabled={!canDinhChinhDiem}
          onClick={() => isEditing ? handleSaveDinhChinh() : setIsEditing(true)}
        >
          {isEditing ? "Lưu Đính Chính" : "Đính Chính Điểm"}
        </Button>
      )}
      </div>
      <div className="rounded-md border">
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead rowSpan={2} className="text-center px-1 border">Id</TableHead>
              {/* <TableHead rowSpan={2} className="text-center">MSSV</TableHead> */}
              <TableHead rowSpan={2} className="text-center px-1 border">Họ và tên</TableHead>
              {components.map((component) => (
                <TableHead
                  key={component.id}
                  colSpan={questions[component.id.toString()]?.length + 1}
                  className="text-center border"
                >
                  {component.loai} ({component.trongSo * 100}%)
                </TableHead>
              ))}
            </TableRow>
            <TableRow>
              {components.flatMap((component) => [
                ...(questions[component.id.toString()] || []).map((question) => (
                  <TableHead key={`${component.loai}_${question.id}`} className="text-center px-1 border">
                    <div>{question.ten}</div>
                    <div>{question.trongSo}</div>
                    <div>{question.thangDiem}</div>
                  </TableHead>
                )),
                <TableHead key={`${component.loai}_total`} className="text-center px-1 border">Tổng</TableHead>
              ])}
            </TableRow>
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows.map((row) => (
              <TableRow key={row.id}>
                {row.getVisibleCells().map((cell) => (
                  <TableCell key={cell.id} className="text-center px-1 border">
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    </div>
  )
}

// interface EditableCellProps {
//   value: number
//   onChange: (value: number) => void
//   isEditing: boolean
// }

function EditableCell({ value, onChange, isEditing }) {
  const [editValue, setEditValue] = React.useState(value.toString())

  React.useEffect(() => {
    setEditValue(value.toString())
  }, [value])

  if (!isEditing) {
    return <span>{value}</span>
  }

  return (
    <Input
      type="number"
      value={editValue}
      onChange={(e) => {
        const newValue = e.target.value;
        // Allow empty, numbers, and single decimal point
        if (newValue === '' || /^\d*\.?\d*$/.test(newValue)) {
          setEditValue(newValue);
          const numValue = parseFloat(newValue);
          if (isNaN(numValue)) {
            onChange(null)
          }
          else {
            onChange(numValue)
          }
        }
      }}
      className="h-8 w-16 text-center"
      min={0}
      max={10}
      step={0.25}
    />
  )
}

