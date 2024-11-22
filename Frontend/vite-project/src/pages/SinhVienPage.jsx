import Layout from "./Layout";
import { getAllSinhViens } from "@/api/api-sinhvien";
import { useEffect, useState } from "react";
import * as React from "react"
import { ArrowUpDown, ChevronDown, MoreHorizontal } from "lucide-react"
import { Button } from "@/components/ui/button"
import { updateSinhVien, deleteSinhVien, addSinhVien } from "@/api/api-sinhvien";
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Input } from "@/components/ui/input"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"

const columns = [
  {
    accessorKey: "id",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Id
          <ArrowUpDown />
        </Button>
      )
    },
    cell: ({ row }) => <div className="px-4 py-2">{row.getValue("id")}</div>,
  },
  {
    accessorKey: "ten",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Tên
          <ArrowUpDown />
        </Button>
      )
    },
    cell: ({ row }) => <div className="px-4 py-2">{row.getValue("ten")}</div>,
  },
    {
      id: "actions",
      enableHiding: false,
      cell: ({ row }) => {
        const student = row.original;
  
        return (
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" className="h-8 w-8 p-0">
                <span className="sr-only">Open menu</span>
                <MoreHorizontal />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>Actions</DropdownMenuLabel>
              <DropdownMenuItem onClick={() => handleEdit(student)}>
                Sửa Sinh Viên
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem onClick={() => handleDelete(student.id)}>
                Xóa Sinh Viên
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        );
      },
    }
  ];


export default function SinhVienPage() {
  const [data, setData] = useState([]);
  const [showAddForm, setShowAddForm] = useState(false);
  const [newStudent, setNewStudent] = useState({ ten: "" });
  const [editingStudent, setEditingStudent] = useState(null);
  const [showEditForm, setShowEditForm] = useState(false);


  useEffect(() => {
    const fetchData = async () => {
      const data = await getAllSinhViens();
      setData(data);
    };
    fetchData();
  }, []);

  const [sorting, setSorting] = useState([]);
  const [columnFilters, setColumnFilters] = useState([]);
  const [columnVisibility, setColumnVisibility] = useState({});
  const [rowSelection, setRowSelection] = useState({});

  const table = useReactTable({
    data,
    columns,
    state: {
      sorting,
      columnFilters,
      columnVisibility,
      rowSelection,
    },
    onSortingChange: setSorting,
    onColumnFiltersChange: setColumnFilters,
    onColumnVisibilityChange: setColumnVisibility,
    onRowSelectionChange: setRowSelection,
    getCoreRowModel: getCoreRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
  });

  const handleCreate = async () => {
    try {
      const createdStudent = await addSinhVien(newStudent);
      setData((prevData) => [...prevData, createdStudent]);
      setNewStudent({ ten: "" });
      setShowAddForm(false);
    } catch (error) {
      console.error("Failed to create student:", error);
    }
  };
  const handleEdit = (student) => {
    setEditingStudent(student);
    setShowEditForm(true);
  };
  
  const handleUpdate = async () => {
    try {
      const updatedStudent = await updateSinhVien(editingStudent.id, editingStudent);
      setData((prevData) =>
        prevData.map((student) =>
          student.id === updatedStudent.id ? updatedStudent : student
        )
      );
      setShowEditForm(false);
    } catch (error) {
      console.error("Failed to update student:", error);
    }
  };
  // ... existing code ...

const handleDelete = async (studentId) => {
  try {
    await deleteSinhVien(studentId);
    setData((prevData) => prevData.filter((student) => student.id !== studentId));
  } catch (error) {
    console.error("Failed to delete student:", error);
  }
};

// ... existing code ...

return (
  <Layout>
    <h1>This is SinhVien Page</h1>
    <div className="w-full">
      <div className="flex items-center py-4">
        <Input
          placeholder="Filter tens..."
          value={(table.getColumn("ten")?.getFilterValue()) ?? ""}
          onChange={(event) =>
            table.getColumn("ten")?.setFilterValue(event.target.value)
          }
          className="max-w-sm"
        />
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="outline" className="ml-auto">
              Columns <ChevronDown />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end">
            {table
              .getAllColumns()
              .filter((column) => column.getCanHide())
              .map((column) => {
                return (
                  <DropdownMenuCheckboxItem
                    key={column.id}
                    className="capitalize"
                    checked={column.getIsVisible()}
                    onCheckedChange={(value) =>
                      column.toggleVisibility(!!value)
                    }
                  >
                    {column.id}
                  </DropdownMenuCheckboxItem>
                );
              })}
          </DropdownMenuContent>
        </DropdownMenu>
        <Button variant="outline" className="ml-2" onClick={() => setShowAddForm(true)}>
          Thêm Sinh Viên
        </Button>
      </div>
      {showAddForm && (
        <div className="mb-4">
          <Input
            placeholder="Student Name"
            value={newStudent.ten}
            onChange={(e) => setNewStudent({ ...newStudent, ten: e.target.value })}
            className="mb-2"
          />
          <Button onClick={handleCreate}>Add Student</Button>
          <Button variant="ghost" onClick={() => setShowAddForm(false)}>Cancel</Button>
        </div>
      )}
      {showEditForm && (
        <div className="mb-4">
          <Input
            placeholder="Student Name"
            value={editingStudent.ten}
            onChange={(e) =>
              setEditingStudent({ ...editingStudent, ten: e.target.value })
            }
            className="mb-2"
          />
          <Button onClick={handleUpdate}>Update Student</Button>
          <Button variant="ghost" onClick={() => setShowEditForm(false)}>Cancel</Button>
        </div>
      )}
      <div className="rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => {
                  return (
                    <TableHead key={header.id}>
                      {header.isPlaceholder
                        ? null
                        : flexRender(
                            header.column.columnDef.header,
                            header.getContext()
                          )}
                    </TableHead>
                  );
                })}
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
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext()
                      )}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell
                  colSpan={columns.length}
                  className="h-24 text-center"
                >
                  No results.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
      <div className="flex items-center justify-end space-x-2 py-4">
        <div className="flex-1 text-sm text-muted-foreground">
          {table.getFilteredSelectedRowModel().rows.length} of{" "}
          {table.getFilteredRowModel().rows.length} row(s) selected.
        </div>
        <div className="space-x-2">
          <Button
            variant="outline"
            size="sm"
            onClick={() => table.previousPage()}
            disabled={!table.getCanPreviousPage()}
          >
            Previous
          </Button>
          <Button
            variant="outline"
            size="sm"
            onClick={() => table.nextPage()}
            disabled={!table.getCanNextPage()}
          >
            Next
          </Button>
        </div>
      </div>
    </div>
  </Layout>
);
}

