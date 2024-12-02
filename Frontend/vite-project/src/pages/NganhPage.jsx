import Layout from "./Layout";
import DataTable from "@/components/DataTable";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";
import { Button } from "@/components/ui/button";
import { getAllKhoas } from "@/api/api-khoa";
import {
  getAllNganhs,
  deleteNganh,
} from "@/api/api-nganh";
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
import { NganhForm } from "@/components/NganhForm";

const createNganhColumns = (handleEdit, handleDelete) => [
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
      );
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
      );
    },
    cell: ({ row }) => <div className="px-4 py-2">{row.getValue("ten")}</div>,
  },
  {
    accessorKey: "khoaId",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Khoa Id
          <ArrowUpDown />
        </Button>
      );
    },
    cell: ({ row }) => <div className="px-4 py-2">{row.getValue("khoaId")}</div>,
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
                  Sửa Ngành
                </DropdownMenuItem>
              </DialogTrigger>
              <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                  <DialogTitle>Edit Nganh</DialogTitle>
                  <DialogDescription>
                    Edit the current item.
                  </DialogDescription>
                </DialogHeader>
                <NganhForm nganhId={item.id} handleEdit={handleEdit} />
              </DialogContent>
            </Dialog>
            <Dialog>
              <DialogTrigger asChild>
                <DropdownMenuItem onSelect={(e) => e.preventDefault()}>
                  Xóa Ngành
                </DropdownMenuItem>
              </DialogTrigger>
              <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                  <DialogTitle>Delete Nganh</DialogTitle>
                  <DialogDescription>
                    Delete the current item.
                  </DialogDescription>
                </DialogHeader>
                <p>Are you sure you want to delete this Nganh?</p>
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

export default function NganhPage() {
  return (
    <Layout>
      <div className="w-full">
        <DataTable
          entity="Nganh"
          createColumns={createNganhColumns}
          getAllItems={getAllNganhs}
          deleteItem={deleteNganh}
          columnToBeFiltered={"ten"}
          ItemForm={NganhForm}
          getAllComboBoxItems={getAllKhoas}
        />
      </div>
    </Layout>
  );
}
