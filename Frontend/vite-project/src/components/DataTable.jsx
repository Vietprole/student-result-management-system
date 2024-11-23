import Layout from "../pages/Layout";
import { useEffect, useState } from "react";
import { ArrowUpDown, ChevronDown, MoreHorizontal } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Input } from "@/components/ui/input";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useReactTable } from "@tanstack/react-table";
import {
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
} from "@tanstack/react-table";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";

// const createColumns = (handleEdit, handleDelete) => [
//   {
//     accessorKey: "id",
//     header: ({ column }) => {
//       return (
//         <Button
//           variant="ghost"
//           onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
//         >
//           Id
//           <ArrowUpDown />
//         </Button>
//       );
//     },
//     cell: ({ row }) => <div className="px-4 py-2">{row.getValue("id")}</div>,
//   },
//   {
//     accessorKey: "ten",
//     header: ({ column }) => {
//       return (
//         <Button
//           variant="ghost"
//           onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
//         >
//           Tên
//           <ArrowUpDown />
//         </Button>
//       );
//     },
//     cell: ({ row }) => <div className="px-4 py-2">{row.getValue("ten")}</div>,
//   },
//   {
//     id: "actions",
//     enableHiding: false,
//     cell: ({ row }) => {
//       const student = row.original;

//       return (
//         <DropdownMenu>
//           <DropdownMenuTrigger asChild>
//             <Button variant="ghost" className="h-8 w-8 p-0">
//               <span className="sr-only">Open menu</span>
//               <MoreHorizontal />
//             </Button>
//           </DropdownMenuTrigger>
//           <DropdownMenuContent align="end">
//             <DropdownMenuLabel>Actions</DropdownMenuLabel>
//             <Dialog>
//               <DialogTrigger asChild>
//                 <DropdownMenuItem onSelect={(e) => e.preventDefault()}>Sửa Sinh Viên</DropdownMenuItem>
//               </DialogTrigger>
//               <DialogContent className="sm:max-w-[425px]">
//                 <DialogHeader>
//                   <DialogTitle>Edit Sinh Vien</DialogTitle>
//                   <DialogDescription>
//                     Edit the current student.
//                   </DialogDescription>
//                 </DialogHeader>
//               <ItemForm itemId={student.id} handleEdit={handleEdit}/>
//               </DialogContent>
//             </Dialog>
//             <Dialog>
//               <DialogTrigger asChild>
//                 <DropdownMenuItem onSelect={(e) => e.preventDefault()}>Xóa Sinh Viên</DropdownMenuItem>
//               </DialogTrigger>
//               <DialogContent className="sm:max-w-[425px]">
//                 <DialogHeader>
//                   <DialogTitle>Delete Sinh Vien</DialogTitle>
//                   <DialogDescription>
//                     Delete the current student.
//                   </DialogDescription>
//                 </DialogHeader>
//                 <p>Are you sure you want to delete this Sinh Vien?</p>
//                 <DialogFooter>
//                   <Button type="submit" onClick={() => handleDelete(student.id)}>Delete</Button>
//                 </DialogFooter>
//               </DialogContent>
//             </Dialog>
//           </DropdownMenuContent>
//         </DropdownMenu>
//       );
//     },
//   },
// ];

export default function DataTable(entity, createColumns, getAllItems, deleteItem, columnToBeFiltered, ItemForm) {
  const [data, setData] = useState([]);
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      const data = await getAllItems();
      setData(data);
    };
    fetchData();
  }, [getAllItems]);

  const [sorting, setSorting] = useState([]);
  const [columnFilters, setColumnFilters] = useState([]);
  const [columnVisibility, setColumnVisibility] = useState({});
  const [rowSelection, setRowSelection] = useState({});

  const handleAdd = (newItem) => {
    console.log("from handleAdd", newItem);
    setData([...data, newItem]);
  };

  const handleEdit = (editedItem) => {
    console.log("from handleEdit", editedItem);
    setData(
      data.map((item) =>
        item.id === editedItem.id ? editedItem : item
      )
    );
  };

  async function handleDelete(itemId) {
    await deleteItem(itemId);
    setData(data.filter((item) => item.id !== itemId));
  }

  const columns = createColumns(handleEdit, handleDelete);

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

  return (
    <Layout>
      <h1>This is {entity} Page</h1>
      <div className="w-full">
        <div className="flex items-center py-4">
          <Input
            placeholder={`Filter ${columnToBeFiltered}s...`}
            value={table.getColumn(`${columnToBeFiltered}`)?.getFilterValue() ?? ""}
            onChange={(event) =>
              table.getColumn(`${columnToBeFiltered}`)?.setFilterValue(event.target.value)
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
          <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
            <DialogTrigger asChild>
              <Button
                variant="outline"
                className="ml-2"
              >
                Thêm {entity}
              </Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[425px]">
              <DialogHeader>
                <DialogTitle>Add {entity}</DialogTitle>
                <DialogDescription>
                  Add a new {entity} to the list.
                </DialogDescription>
              </DialogHeader>
            <ItemForm handleAdd={handleAdd} setIsDialogOpen={setIsDialogOpen}/>
            </DialogContent>
          </Dialog>
        </div>
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
