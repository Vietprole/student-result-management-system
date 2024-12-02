import DataTable from "@/components/DataTable";
import { useParams } from "react-router-dom";
import { ArrowUpDown, MoreHorizontal, ChevronDown } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuItem,
} from "@/components/ui/dropdown-menu";
import { getBaiKiemTrasByLopHocPhanId, deleteBaiKiemTra } from "@/api/api-baikiemtra";
import { useEffect, useState } from "react";
import { getCauHoisByBaiKiemTraId, deleteCauHoi } from "@/api/api-cauhoi";
import { BaiKiemTraForm } from "@/components/BaiKiemTraForm";
import { CauHoiForm } from "@/components/CauHoiForm";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";

function createBaiKiemTraColumns(handleEdit, handleDelete){
  return [
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
      accessorKey: "loai",
      header: ({ column }) => (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Loại
          <ArrowUpDown />
        </Button>
      ),
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("loai")}</div>,
    },
    {
      accessorKey: "trongSo",
      header: ({ column }) => (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Trọng số
          <ArrowUpDown />
        </Button>
      ),
      cell: ({ row }) => (
        <div className="px-4 py-2">{row.getValue("trongSo")}</div>
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
                <DropdownMenuItem onSelect={(e) => e.preventDefault()}>Sửa Bài Kiểm Tra</DropdownMenuItem>
              </DialogTrigger>
              <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                  <DialogTitle>Edit Bai Kiem Tra</DialogTitle>
                  <DialogDescription>
                    Edit the current Bai Kiểm Tra.
                  </DialogDescription>
                </DialogHeader>
              <BaiKiemTraForm baiKiemTraId={item.id} handleEdit={handleEdit}/>
              </DialogContent>
            </Dialog>
            <Dialog>
              <DialogTrigger asChild>
                <DropdownMenuItem onSelect={(e) => e.preventDefault()}>Xóa Bài Kiểm Tra</DropdownMenuItem>
              </DialogTrigger>
              <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                  <DialogTitle>Delete Bai Kiem Tra</DialogTitle>
                  <DialogDescription>
                    Delete the current Bai Kiem Tra.
                  </DialogDescription>
                </DialogHeader>
                <p>Are you sure you want to delete this Bai Kiem Tra?</p>
                <DialogFooter>
                  <Button type="submit" onClick={() => handleDelete(item.id)}>Delete</Button>
                </DialogFooter>
              </DialogContent>
            </Dialog>
          </DropdownMenuContent>
        </DropdownMenu>
        );
      },
    },
  ];
}

function createCauHoiColumns(handleEdit, handleDelete){
  return [
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
      accessorKey: "trongSo",
      header: ({ column }) => (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Trọng số
          <ArrowUpDown />
        </Button>
      ),
      cell: ({ row }) => (
        <div className="px-4 py-2">{row.getValue("trongSo")}</div>
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
                <DropdownMenuItem onSelect={(e) => e.preventDefault()}>Sửa Câu Hỏi</DropdownMenuItem>
              </DialogTrigger>
              <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                  <DialogTitle>Edit Bai Kiem Tra</DialogTitle>
                  <DialogDescription>
                    Edit the current Bai Kiểm Tra.
                  </DialogDescription>
                </DialogHeader>
              <CauHoiForm cauHoiId={item.id} handleEdit={handleEdit}/>
              </DialogContent>
            </Dialog>
            <Dialog>
              <DialogTrigger asChild>
                <DropdownMenuItem onSelect={(e) => e.preventDefault()}>Xóa Câu Hỏi</DropdownMenuItem>
              </DialogTrigger>
              <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                  <DialogTitle>Delete Cau Hoi</DialogTitle>
                  <DialogDescription>
                    Delete the current Cau Hoi.
                  </DialogDescription>
                </DialogHeader>
                <p>Are you sure you want to delete this Cau Hoi?</p>
                <DialogFooter>
                  <Button type="submit" onClick={() => handleDelete(item.id)}>Delete</Button>
                </DialogFooter>
              </DialogContent>
            </Dialog>
          </DropdownMenuContent>
        </DropdownMenu>
        );
      },
    },
  ];
}

const CongThucDiem = () => {
  const { lopHocPhanId } = useParams();
  const [baiKiemTras, setBaiKiemTras] = useState([]);
  const [cauHois, setCauHois] = useState([]);
  const [selectedBaiKiemTra, setSelectedBaiKiemTra] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      const baiKiemTrasData = await getBaiKiemTrasByLopHocPhanId(lopHocPhanId);
      setBaiKiemTras(baiKiemTrasData);
    };
    fetchData();
  }, [lopHocPhanId]);

  return (
    <div>
      Công Thức Điểm Component for Lớp Học Phần ID: {lopHocPhanId}
      <div className="flex">
        <div className="w-1/2 p-2">
          <DataTable
            entity="Bai Kiem Tra"
            createColumns={createBaiKiemTraColumns}
            getAllItems={() => getBaiKiemTrasByLopHocPhanId(lopHocPhanId)}
            deleteItem={deleteBaiKiemTra}
            columnToBeFiltered={"loai"}
            ItemForm={BaiKiemTraForm}
          />
        </div>
        <div className="w-1/2 p-2">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline">
                {selectedBaiKiemTra
                  ? selectedBaiKiemTra.loai
                  : "Select Bai Kiem Tra"}
                <ChevronDown />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent>
              {baiKiemTras.map((baiKiemTra) => (
                <DropdownMenuItem
                  key={baiKiemTra.id}
                  onSelect={() => {
                    setSelectedBaiKiemTra(baiKiemTra);
                    const fetchData = async () => {
                      const cauHoisData = await getCauHoisByBaiKiemTraId(
                        baiKiemTra.id
                      );
                      setCauHois(cauHoisData);
                    };
                    fetchData();
                  }}
                >
                  {baiKiemTra.loai}
                </DropdownMenuItem>
              ))}
            </DropdownMenuContent>
          </DropdownMenu>
          <DataTable
            entity="Cau Hoi"
            createColumns={createCauHoiColumns}
            getAllItems={() => getCauHoisByBaiKiemTraId(selectedBaiKiemTra.id)}
            deleteItem={deleteCauHoi}
            columnToBeFiltered={"ten"}
            ItemForm={CauHoiForm}
          />
        </div>
      </div>
    </div>
  );
};

export default CongThucDiem;
