"use client"

import React from "react"
import {
  ColumnDef,
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

// Define types for PLO and CLO
type PLO = {
  id: number
  ten: string
  moTa: string
  ctdtId: number
}

type CLO = {
  id: number
  ten: string
  moTa: string
  lopHocPhanId: number
}

// Sample data
const plos: PLO[] = [
  { id: 1, ten: "PLO 1", moTa: "PLO 1", ctdtId: 1 },
  { id: 2, ten: "PLO 2", moTa: "PLO 2", ctdtId: 1 },
  { id: 3, ten: "PLO 3", moTa: "PLO 3", ctdtId: 1 },
]

const clos: CLO[] = [
  { id: 1, ten: "CLO 1", moTa: "Kỹ Năng Thuyết Trình", lopHocPhanId: 1 },
  { id: 2, ten: "CLO 2", moTa: "Kỹ Năng Làm Việc Nhóm", lopHocPhanId: 1 },
  { id: 3, ten: "CLO 3", moTa: "Kỹ Năng Tìm Kiếm", lopHocPhanId: 1 },
  { id: 4, ten: "CLO 4", moTa: "Kỹ Năng Ngoại Ngữ", lopHocPhanId: 1 },
]

// Mock API data
let mockApiData: Record<number, CLO[]> = {
  1: [
    { id: 1, ten: "CLO 1", moTa: "Kỹ Năng Thuyết Trình", lopHocPhanId: 1 },
    { id: 3, ten: "CLO 3", moTa: "Kỹ Năng Tìm Kiếm", lopHocPhanId: 1 },
  ],
  2: [
    { id: 2, ten: "CLO 2", moTa: "Kỹ Năng Làm Việc Nhóm", lopHocPhanId: 1 },
    { id: 4, ten: "CLO 4", moTa: "Kỹ Năng Ngoại Ngữ", lopHocPhanId: 1 },
  ],
  3: [
    { id: 1, ten: "CLO 1", moTa: "Kỹ Năng Thuyết Trình", lopHocPhanId: 1 },
    { id: 2, ten: "CLO 2", moTa: "Kỹ Năng Làm Việc Nhóm", lopHocPhanId: 1 },
    { id: 3, ten: "CLO 3", moTa: "Kỹ Năng Tìm Kiếm", lopHocPhanId: 1 },
  ],
}

// Mock API function
const fetchPLOData = async (ploId: number): Promise<CLO[]> => {
  await new Promise(resolve => setTimeout(resolve, 500)) // Simulate API delay
  return mockApiData[ploId] || []
}

// Custom cell component
const ToggleableCell = ({ row, column, table }: any) => {
  const ploId = column.id
  const cloId = row.original.id
  const [toggled, setToggled] = React.useState(false)
  const isEditable = table.options.meta?.isEditable

  React.useEffect(() => {
    const checkToggleStatus = async () => {
      const ploData = await fetchPLOData(parseInt(ploId))
      setToggled(ploData.some(clo => clo.id === cloId))
    }
    checkToggleStatus()
  }, [ploId, cloId])

  const onToggle = () => {
    if (isEditable) {
      setToggled(!toggled)
      table.options.meta?.updateData(cloId, ploId, !toggled)
    }
  }

  return (
    <div
      className={`p-2 ${
        toggled ? "bg-blue-500 text-white" : "bg-white text-black"
      } ${isEditable ? "cursor-pointer" : "cursor-not-allowed opacity-50"}`}
      onClick={onToggle}
    >
      {toggled ? "✓" : ""}
    </div>
  )
}

export default function PLOCLOTable() {
  const [tableData, setTableData] = React.useState<Record<string, boolean>>({})
  const [isEditable, setIsEditable] = React.useState(true)

  const columns: ColumnDef<CLO>[] = [
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
      updateData: (cloId: number, ploId: string, toggled: boolean) => {
        setTableData(prev => ({
          ...prev,
          [`${cloId}-${ploId}`]: toggled,
        }))
      },
      isEditable,
    },
  })

  const saveChanges = () => {
    const newMockApiData: Record<number, CLO[]> = {}
    
    Object.entries(tableData).forEach(([key, value]) => {
      const [cloId, ploId] = key.split('-').map(Number)
      if (value) {
        if (!newMockApiData[ploId]) {
          newMockApiData[ploId] = []
        }
        const clo = clos.find(c => c.id === cloId)
        if (clo) {
          newMockApiData[ploId].push(clo)
        }
      }
    })

    mockApiData = newMockApiData
    console.log("Updated mockApiData:", mockApiData)
    setIsEditable(false)
    alert("Changes saved successfully! Table is now read-only.")
  }

  const enableEditing = () => {
    setIsEditable(true)
  }

  return (
    <div className="space-y-4">
      {isEditable ? (
        <Button onClick={saveChanges} variant="outline">
          Save Changes
        </Button>
      ) : (
        <Button onClick={enableEditing} variant="outline">
          Enable Editing
        </Button>
      )}
      <div className="rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableHead key={header.id}>
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

