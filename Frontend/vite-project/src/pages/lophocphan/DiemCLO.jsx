import * as React from "react"
import {
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getSortedRowModel,
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
import { ArrowUpDown } from 'lucide-react'
import { calculateDiemCLO } from "@/api/api-ketqua"

const CLOs = [
  {
    "id": 1,
    "ten": "CLO 1",
    "moTa": "Kỹ Năng Làm Việc Nhóm",
    "lopHocPhanId": 1
  },
  {
    "id": 9,
    "ten": "CLO 2",
    "moTa": "Kỹ Năng Ngoại Ngữ",
    "lopHocPhanId": 1
  },
  {
    "id": 10,
    "ten": "CLO 3",
    "moTa": "Kỹ Năng Giao Tiếp",
    "lopHocPhanId": 1
  }
]

const sinhViens = [
  {
    "id": 8,
    "ten": "Lê Phan Phú Việt"
  },
  {
    "id": 9,
    "ten": "Huỳnh Duy Tin"
  },
  {
    "id": 10,
    "ten": "Hà Ngọc Hưng"
  },
  {
    "id": 11,
    "ten": "Phạm Minh Quân"
  }
]

const columns = [
  {
    accessorKey: "id",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          ID
          <ArrowUpDown className="ml-2 h-4 w-4" />
        </Button>
      )
    },
  },
  {
    accessorKey: "ten",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Name
          <ArrowUpDown className="ml-2 h-4 w-4" />
        </Button>
      )
    },
  },
  ...CLOs.map(clo => ({
    accessorKey: `clo_${clo.id}`,
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          {clo.ten}
          <ArrowUpDown className="ml-2 h-4 w-4" />
        </Button>
      )
    },
    cell: ({ row }) => {
      const score = row.original[`clo_${clo.id}`]
      return score.toFixed(2)
    }
  }))
]

export default function DiemCLO() {
  const [data, setData] = React.useState([])
  const [columnFilters, setColumnFilters] = React.useState([])
  const [sorting, setSorting] = React.useState([])

  React.useEffect(() => {
    const fetchData = async () => {
      const newData = await Promise.all(sinhViens.map(async (sv) => {
        const cloScores = await Promise.all(CLOs.map(async (clo) => {
          const score = await calculateDiemCLO(sv.id, clo.id)
          return { [`clo_${clo.id}`]: score }
        }))
        return { ...sv, ...Object.assign({}, ...cloScores) }
      }))
      setData(newData)
    }
    fetchData()
  }, [])

  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    onColumnFiltersChange: setColumnFilters,
    getFilteredRowModel: getFilteredRowModel(),
    onSortingChange: setSorting,
    getSortedRowModel: getSortedRowModel(),
    state: {
      columnFilters,
      sorting,
    },
  })

  return (
    <div>
      <div className="flex items-center py-4">
        <Input
          placeholder="Filter names..."
          value={(table.getColumn("ten")?.getFilterValue()) ?? ""}
          onChange={(event) =>
            table.getColumn("ten")?.setFilterValue(event.target.value)
          }
          className="max-w-sm"
        />
      </div>
      <div className="rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableHead key={header.id}>
                    {header.isPlaceholder
                      ? null
                      : flexRender(
                          header.column.columnDef.header,
                          header.getContext()
                        )}
                  </TableHead>
                ))}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows?.length ? (
              table.getRowModel().rows.map((row) => (
                <TableRow
                  key={row.id}
                  data-state={row.getIsSelected() && "selected"}
                >
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>
                      {flexRender(cell.column.columnDef.cell, cell.getContext())}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell colSpan={columns.length} className="h-24 text-center">
                  No results.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
    </div>
  )
}

