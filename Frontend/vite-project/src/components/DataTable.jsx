// import Layout from "../pages/Layout";
import { useEffect, useState } from "react";
import { ChevronDown } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  // DropdownMenuItem,
  // DropdownMenuLabel,
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
  // DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { ComboBox } from "./ComboBox";

export default function DataTable({
  entity,
  createColumns,
  getAllItems,
  deleteItem,
  columnToBeFiltered,
  ItemForm,
  getAllComboBoxItems,
}) {
  const [data, setData] = useState([]);
  const [comboBoxItems, setComboBoxItems] = useState([]);
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      if (getAllComboBoxItems) {
        const comboBoxItems = await getAllComboBoxItems();
        const mappedComboBoxItems = comboBoxItems.map(({ ten, ...rest }) => ({
          ...rest,
          label: ten
        }));
        console.log("mapped", mappedComboBoxItems);
        setComboBoxItems(mappedComboBoxItems);
      }

      const data = await getAllItems();
      setData(data);
    };
    fetchData();
  }, [getAllItems, getAllComboBoxItems]);

  const [sorting, setSorting] = useState([]);
  const [columnFilters, setColumnFilters] = useState([]);
  const [columnVisibility, setColumnVisibility] = useState({});
  const [rowSelection, setRowSelection] = useState({});

  const handleAdd = (newItem) => {
    setData([...data, newItem]);
  };

  const handleEdit = (editedItem) => {
    setData(
      data.map((item) => (item.id === editedItem.id ? editedItem : item))
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
    <>
      <h1>This is {entity} Page</h1>
      <div className="w-full">
        {getAllComboBoxItems && (
          <>
            <ComboBox
              items={comboBoxItems}
              // value={table.getColumn("khoaId")?.getFilterValue() ?? ""}
              // onChange={(event) =>
              //   table.getColumn("khoaId")?.setFilterValue(event.target.value)
              // }
            />
            <Button>
              Go
            </Button>
          </>
        )}
        <div className="flex items-center py-4">
          <Input
            placeholder={`Filter ${columnToBeFiltered}s...`}
            value={
              table.getColumn(`${columnToBeFiltered}`)?.getFilterValue() ?? ""
            }
            onChange={(event) =>
              table
                .getColumn(`${columnToBeFiltered}`)
                ?.setFilterValue(event.target.value)
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
              <Button variant="outline" className="ml-2">
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
              <ItemForm
                handleAdd={handleAdd}
                setIsDialogOpen={setIsDialogOpen}
              />
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
    </>
  );
}
