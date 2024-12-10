import Layout from "./Layout";
import DataTable from "@/components/DataTable";
import { useEffect, useState, useRef } from "react";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";
import { Button } from "@/components/ui/button";
import { getAllCTDTs, deleteCTDT } from "@/api/api-ctdt";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
  DropdownMenuSeparator,
} from "@/components/ui/dropdown-menu";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
  DialogClose,
} from "@/components/ui/dialog";
import { CTDTForm } from "@/components/CTDTForm";
import { getNganhs } from "@/api/api-nganh";
import ComboBox from "@/components/ComboBox";

export default function CTDTPage() {
  const addHocPhanFormRef = useRef(null);
  const manageHocPhanFormRef = useRef(null);
  const [nganhItems, setNganhItems] = useState([]);
  const [selectedNganhId, setSelectedNganhId] = useState(null);

  useEffect(() => {
    const fetchNganhs = async () => {
      const dataNganh = await getNganhs();
      const mappedNganhItems = dataNganh.map(nganh => ({ label: nganh.ten, value: nganh.id }));
      setNganhItems(mappedNganhItems);
    };
    fetchNganhs();
  }, []);

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
      cell: ({ row }) => (
        <div className="px-4 py-2">{row.getValue("nganhId")}</div>
      ),
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
                  <CTDTForm cTDT={item} handleEdit={handleEdit} />
                </DialogContent>
              </Dialog>
              <Dialog>
                <DialogTrigger asChild>
                  <DropdownMenuItem onSelect={(e) => e.preventDefault()}>
                    Xóa Chương Trình
                  </DropdownMenuItem>
                </DialogTrigger>
                <DropdownMenuSeparator />
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle>Delete Chương Trình</DialogTitle>
                    <DialogDescription>
                      Delete the current Chương Trình.
                    </DialogDescription>
                  </DialogHeader>
                  <p>Are you sure you want to delete this Chương Trình?</p>
                  <DialogFooter>
                    <Button type="submit" onClick={() => handleDelete(item.id)}>
                      Delete
                    </Button>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
              <Dialog>
                <DialogTrigger asChild>
                  <DropdownMenuItem onSelect={(e) => e.preventDefault()}>
                    Thêm Học Phần
                  </DropdownMenuItem>
                </DialogTrigger>
                <DialogContent className="w-auto max-w-none">
                  <DialogHeader>
                    <DialogTitle>
                      Thêm học phần có sẵn vào Chương trình đào tạo
                    </DialogTitle>
                  </DialogHeader>
                  <AddHocPhanToCTDTForm
                    ref={addHocPhanFormRef}
                    cTDTId={item.id}
                  />
                  <DialogFooter>
                    <DialogClose asChild>
                      <Button
                        type="button"
                        variant="default"
                        onClick={() =>
                          addHocPhanFormRef.current.handleAddHocPhan()
                        }
                      >
                        Thêm
                      </Button>
                    </DialogClose>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
              <Dialog>
                <DialogTrigger asChild>
                  <DropdownMenuItem onSelect={(e) => e.preventDefault()}>
                    Quản lý Học Phần
                  </DropdownMenuItem>
                </DialogTrigger>
                <DialogContent className="w-auto max-w-none">
                  <DialogHeader>
                    <DialogTitle>
                      Xem và xóa học phần khỏi chương trình đào tạo
                    </DialogTitle>
                  </DialogHeader>
                  <ManageHocPhanInCTDTForm
                    ref={manageHocPhanFormRef}
                    cTDTId={item.id}
                  />
                  <DialogFooter>
                    <DialogClose asChild>
                      <Button
                        type="button"
                        variant="default"
                        onClick={() =>
                          manageHocPhanFormRef.current.handleRemoveHocPhan()
                        }
                      >
                        Xóa
                      </Button>
                    </DialogClose>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
            </DropdownMenuContent>
          </DropdownMenu>
        );
      },
    },
  ];
  
  return (
    <Layout>
      <div className="w-full">
        <ComboBox items={nganhItems} setItemId={setSelectedNganhId} initialItemId={selectedNganhId} />
        <DataTable
          entity="Chương Trình"
          createColumns={createCTDTColumns}
          getAllItems={() => getAllCTDTs()}
          deleteItem={deleteCTDT}
          columnToBeFiltered={"ten"}
          ItemForm={CTDTForm}
        />
      </div>
    </Layout>
  );
}
