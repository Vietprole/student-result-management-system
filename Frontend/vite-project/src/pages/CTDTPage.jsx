import Layout from "./Layout";
import DataTable from "@/components/DataTable";
// import { useEffect, useState } from "react";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  getAllCTDT,
  deleteCTDT,
} from "@/api/api-ctdt";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { CTDTForm } from "@/components/CTDTForm";

const createCTDTColumns = (handleEdit, handleDelete) => [
  {
    accessorKey: "id",
    header: ({ column }) => (
      <Button
        variant="ghost"
        onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
      >
        Id
        <ArrowUpDown />
      </Button>
    ),
    cell: ({ row }) => <div className="px-4 py-2">{row.getValue("id")}</div>,
  },
  {
    accessorKey: "ten",
    header: ({ column }) => (
      <Button
        variant="ghost"
        onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
      >
        Tên
        <ArrowUpDown />
      </Button>
    ),
    cell: ({ row }) => <div className="px-4 py-2">{row.getValue("ten")}</div>,
  },
  {
    accessorKey: "nganhId",
    header: ({ column }) => (
      <Button
        variant="ghost"
        onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
      >
        Ngành Id
        <ArrowUpDown />
      </Button>
    ),
    cell: ({ row }) => <div className="px-4 py-2">{row.getValue("nganhId")}</div>,
  },
  {
    id: "actions",
    enableHiding: false,
    cell: ({ row }) => {
      const item = row.original;

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
            <Dialog>
              <DialogTrigger asChild>
                <DropdownMenuItem onSelect={(e) => e.preventDefault()}>
                  Sửa Chương Trình
                </DropdownMenuItem>
              </DialogTrigger>
              <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                  <DialogTitle>Edit Chương Trình</DialogTitle>
                  <DialogDescription>
                    Edit the current Chương Trình.
                  </DialogDescription>
                </DialogHeader>
                <CTDTForm cTDTId={item.id} handleEdit={handleEdit} />
              </DialogContent>
            </Dialog>
            <Dialog>
              <DialogTrigger asChild>
                <DropdownMenuItem onSelect={(e) => e.preventDefault()}>
                  Xóa Chương Trình
                </DropdownMenuItem>
              </DialogTrigger>
              <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                  <DialogTitle>Delete Chương Trình</DialogTitle>
                  <DialogDescription>
                    Delete the current Chương Trình.
                  </DialogDescription>
                </DialogHeader>
                <p>Are you sure you want to delete this Chương Trình?</p>
                <DialogFooter>
                  <Button
                    type="submit"
                    onClick={() => handleDelete(item.id)}
                  >
                    Delete
                  </Button>
                </DialogFooter>
              </DialogContent>
            </Dialog>
          </DropdownMenuContent>
        </DropdownMenu>
      );
    },
  },
];

export default function CTDTPage() {
  return (
    <Layout>
      <div className="w-full">
        <DataTable
          entity="Chương Trình"
          createColumns={createCTDTColumns}
          getAllItems={() => getAllCTDT()}
          deleteItem={deleteCTDT}
          columnToBeFiltered={"ten"}
          ItemForm={CTDTForm}
        />
      </div>
    </Layout>
  );
}
