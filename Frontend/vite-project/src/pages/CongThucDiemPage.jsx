import Layout from "./Layout";
import DataTable from "@/components/DataTable";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";
import { Button } from "@/components/ui/button";
import { getAllLopHocPhans } from "@/api/api-lophocphan";
import {
  getBaiKiemTrasByLopHocPhanId,
  // getAllBaiKiemTras,
  deleteBaiKiemTra,
} from "@/api/api-baikiemtra";
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
import { BaiKiemTraForm } from "@/components/BaiKiemTraForm";
import { useState, useEffect, useCallback } from "react";
import { ComboBox } from "@/components/ComboBox";
import { useSearchParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";

export default function BaiKiemTraPage() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const lophocphanIdParam = searchParams.get("lophocphanId");
  const [data, setData] = useState([]);
  const [lophocphanItems, setLopHocPhanItems] = useState([]);
  const [lophocphanId, setLopHocPhanId] = useState(lophocphanIdParam);
  const [comboBoxLopHocPhanId, setComboBoxLopHocPhanId] = useState(lophocphanIdParam);

  const fetchData = useCallback(async () => {
    const dataLopHocPhan = await getAllLopHocPhans();
    // Map lophocphan items to be used in ComboBox
    const mappedComboBoxItems = dataLopHocPhan.map(lophocphan => ({ label: lophocphan.ten, value: lophocphan.id }));
    setLopHocPhanItems(mappedComboBoxItems);
    const data = await getBaiKiemTrasByLopHocPhanId(lophocphanId);
    setData(data);
  }, [lophocphanId]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  const handleGoClick = () => {
    setLopHocPhanId(comboBoxLopHocPhanId);
    if (comboBoxLopHocPhanId === null) {
      navigate(`/baikiemtra`);
      return;
    }
    navigate(`/baikiemtra?lophocphanId=${comboBoxLopHocPhanId}`);
  };

  const createBaiKiemTraColumns = (handleEdit, handleDelete) => [
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
      accessorKey: "loai",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Loại
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("loai")}</div>,
    },
    {
      accessorKey: "trongSo",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Trọng Số
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("trongSo")}</div>,
    },
    {
      accessorKey: "trongSoDeXuat",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Trọng Số Đề Xuất
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("trongSoDeXuat")}</div>,
    },
    {
      accessorKey: "ngayMoNhapDiem",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Ngày Mở Nhập Điểm
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("ngayMoNhapDiem")}</div>,
    },
    {
      accessorKey: "hanNhapDiem",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Hạn Nhập Điểm
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("hanNhapDiem")}</div>,
    },
    {
      accessorKey: "hanDinhChinh",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Hạn Đính Chính
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("hanDinhChinh")}</div>,
    },
    {
      accessorKey: "ngayXacNhan",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Ngày Xác Nhận
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("ngayXacNhan")}</div>,
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
                    <DialogTitle>Edit BaiKiemTra</DialogTitle>
                    <DialogDescription>
                      Edit the current item.
                    </DialogDescription>
                  </DialogHeader>
                  <BaiKiemTraForm baikiemtra={item} handleEdit={handleEdit} />
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
                    <DialogTitle>Delete BaiKiemTra</DialogTitle>
                    <DialogDescription>
                      Delete the current item.
                    </DialogDescription>
                  </DialogHeader>
                  <p>Are you sure you want to delete this BaiKiemTra?</p>
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

  return (
    <Layout>
      <div className="w-full">
        <div className="flex">
          <ComboBox items={lophocphanItems} setItemId={setComboBoxLopHocPhanId} initialItemId={comboBoxLopHocPhanId}/>
          <Button onClick={handleGoClick}>Go</Button>
        </div>
        <DataTable
          entity="BaiKiemTra"
          createColumns={createBaiKiemTraColumns}
          data={data}
          setData={setData}
          fetchData={fetchData}
          deleteItem={deleteBaiKiemTra}
          columnToBeFiltered={"ten"}
          ItemForm={BaiKiemTraForm}
        />
      </div>
    </Layout>
  );
}
