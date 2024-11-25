import React from "react"
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
import { Button } from "@/components/ui/button"

// Sample data
const plos = [
  { id: 1, ten: "PLO 1", moTa: "PLO 1", ctdtId: 1 },
  { id: 2, ten: "PLO 2", moTa: "PLO 2", ctdtId: 1 },
  { id: 3, ten: "PLO 3", moTa: "PLO 3", ctdtId: 1 },
]

const clos = [
  { id: 1, ten: "CLO 1", moTa: "Kỹ Năng Thuyết Trình", lopHocPhanId: 1 },
  { id: 2, ten: "CLO 2", moTa: "Kỹ Năng Làm Việc Nhóm", lopHocPhanId: 1 },
  { id: 3, ten: "CLO 3", moTa: "Kỹ Năng Tìm Kiếm", lopHocPhanId: 1 },
  { id: 4, ten: "CLO 4", moTa: "Kỹ Năng Ngoại Ngữ", lopHocPhanId: 1 },
]

// Mock API function
const fetchPLOData = async (ploId) => {
  // This is a mock implementation. Replace with actual API call in production.
  await new Promise(resolve => setTimeout(resolve, 500)) // Simulate API delay
  if (ploId === 1) {
    return [
      { id: 1, ten: "CLO 1", moTa: "Kỹ Năng Thuyết Trình", lopHocPhanId: 1 },
      { id: 3, ten: "CLO 3", moTa: "Kỹ Năng Tìm Kiếm", lopHocPhanId: 1 },
    ]
  }
  return []
}

// Custom cell component
const ToggleableCell = ({ row, column, table }) => {
  const ploId = column.id
  const cloId = row.original.id
  const [toggled, setToggled] = React.useState(false)
  const cellsToggleable = table.options.meta?.cellsToggleable

  React.useEffect(() => {
    const checkToggleStatus = async () => {
      const ploData = await fetchPLOData(parseInt(ploId))
      setToggled(ploData.some(clo => clo.id === cloId))
    }
    checkToggleStatus()
  }, [ploId, cloId])

  const onToggle = () => {
    if (cellsToggleable) {
      setToggled(!toggled)
      table.options.meta?.updateData(cloId, ploId, !toggled)
    }
  }

  return (
    <div
      className={`cursor-pointer p-2 ${
        toggled ? "bg-blue-500 text-white" : "bg-white text-black"
      } ${cellsToggleable ? "" : "cursor-not-allowed opacity-50"}`}
      onClick={onToggle}
    >
      {toggled ? "✓" : ""}
    </div>
  )
}

export default function PLOCLOTable() {
  const [cellsToggleable, setCellsToggleable] = React.useState(true)

  const columns = [
    {
      accessorKey: "ten",
      header: "CLO",
    },
    ...plos.map(plo => ({
      accessorKey: plo.id.toString(),
      header: plo.ten,
      cell: ToggleableCell,
    })),
  ]

  const table = useReactTable({
    data: clos,
    columns,
    getCoreRowModel: getCoreRowModel(),
    meta: {
      updateData: (cloId, ploId, toggled) => {
        console.log(`CLO ${cloId} for PLO ${ploId} toggled: ${toggled}`)
        // Here you would typically update your backend or state management
      },
      cellsToggleable,
    },
  })

  return (
    <div className="space-y-4">
      <Button
        onClick={() => setCellsToggleable(!cellsToggleable)}
        variant="outline"
      >
        {cellsToggleable ? "Disable" : "Enable"} Toggleable Cells
      </Button>
      <div className="rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableHead key={header.id} className="">
                    {flexRender(
                      header.column.columnDef.header,
                      header.getContext()
                    )}
                  </TableHead>
                ))}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows.map((row) => (
              <TableRow key={row.id}>
                {row.getVisibleCells().map((cell) => (
                  <TableCell key={cell.id}>
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

