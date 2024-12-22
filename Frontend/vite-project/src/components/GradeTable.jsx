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
import { upsertKetQua } from "@/api/api-ketqua"
import { upsertDiemDinhChinh } from "@/api/api-diemdinhchinh"

export function GradeTable({
  data,
  components,
  questions,
  isGiangVienMode,
}) {
  const [tableData, setTableData] = React.useState(data)
  const [isEditing, setIsEditing] = React.useState(false)
  const [modifiedRecords, setModifiedRecords] = React.useState([])
  const [modifiedDiemDinhChinhRecords, setModifiedDiemDinhChinhRecords] = React.useState([])

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
              row.grades[component.loai]?.[question.id.toString()] ?? 0,
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
                  const rowIndex = row.index
                  const [componentId, questionId] = column.id.split("_")
                  
                  if (!newData[rowIndex].grades[componentId]) {
                    newData[rowIndex].grades[componentId] = {}
                  }
                  
                  newData[rowIndex].grades[componentId][questionId] = value
                  const ketQuaId = newData[rowIndex].ketQuas[componentId][questionId];
                  const modifiedRecord = {
                    sinhVienId: newData[rowIndex].id,
                    cauHoiId: parseInt(questionId),
                    diemTam: value,
                  }

                  const modifiedDiemDinhChinhRecord = {
                    ketQuaId: ketQuaId,
                    diemMoi: value,
                  }
                  console.log("modifiedRecord", modifiedRecord);
                  console.log("modifiedDiemDinhChinhRecord", modifiedDiemDinhChinhRecord);
                  // upsertKetQua(modifiedRecord);

                  setModifiedRecords([...modifiedRecords, modifiedRecord]);
                  setModifiedDiemDinhChinhRecords([...modifiedDiemDinhChinhRecords, modifiedDiemDinhChinhRecord]);

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
      } catch (error) {
        console.error(`Error updating Diem Dinh Chinh record for ketQuaId ${modifiedDiemDinhChinhRecords[i].ketQuaId}:`, error);
      }
    }
  }

  // function that compare date to today and return the result
  const isDatePassed = (dateString) => {
    const today = new Date();
    const date = new Date(dateString);
    return date.getTime() < today.getTime();
  }

  const canEditDiem = !isGiangVienMode || (isGiangVienMode && isDatePassed(components[0].ngayMoNhapDiem) && !isDatePassed(components[0].hanNhapDiem));
  const canDinhChinhDiem = isGiangVienMode && isDatePassed(components[0].hanNhapDiem) && !isDatePassed(components[0].hanDinhChinh);

  return (
    <div className="space-y-4">
      <div className="flex justify-end">
      {canEditDiem && (
        <Button
          onClick={() => isEditing ? handleSaveChanges() : setIsEditing(true)}
        >
          {isEditing ? "Lưu" : "Sửa Điểm"}
        </Button>
      )}
      {canDinhChinhDiem && (
        <Button
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
                  className="text-center"
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
        const newValue = e.target.value
        setEditValue(newValue)
        const numValue = parseFloat(newValue) || 0
        onChange(numValue)
      }}
      className="h-8 w-16 text-center"
      min={0}
      max={10}
      step={0.25}
    />
  )
}

