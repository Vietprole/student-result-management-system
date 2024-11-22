import DataTable from "@/components/DataTable";
import { useParams } from "react-router-dom";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";
import { Button } from "@/components/ui/button";
// import { Checkbox } from '@/components/ui/checkbox';
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuItem,
} from "@/components/ui/dropdown-menu";
import { getBaiKiemTraByLopHocPhanId } from "@/api/api-baikiemtra";
import { useEffect, useState } from "react";
import { getCauHoiByBaiKiemTraId } from "@/api/api-cauhoi";
import { ChevronDown } from "lucide-react";

const baiKiemTraColumns = [
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
          Trọng số
          <ArrowUpDown />
        </Button>
      );
    },
    cell: ({ row }) => (
      <div className="px-4 py-2">{row.getValue("trongSo")}</div>
    ),
  },
  {
    id: "actions",
    enableHiding: false,
    cell: () => {
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
            <DropdownMenuItem>Sửa</DropdownMenuItem>
            <DropdownMenuItem>Xóa</DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      );
    },
  },
];

const cauHoiColumns = [
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
    accessorKey: "trongSo",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Trọng số
          <ArrowUpDown />
        </Button>
      );
    },
    cell: ({ row }) => (
      <div className="px-4 py-2">{row.getValue("trongSo")}</div>
    ),
  },
  {
    id: "actions",
    enableHiding: false,
    cell: () => {
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
            <DropdownMenuItem>Sửa</DropdownMenuItem>
            <DropdownMenuItem>Xóa</DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      );
    },
  },
];

const CongThucDiem = () => {
  const { lopHocPhanId } = useParams();
  const [baiKiemTras, setBaiKiemTras] = useState([]);
  const [cauHois, setCauHois] = useState([]);
  const [selectedBaiKiemTra, setSelectedBaiKiemTra] = useState(null);
  useEffect(() => {
    const fetchData = async () => {
      const baiKiemTrasData = await getBaiKiemTraByLopHocPhanId(lopHocPhanId);
      setBaiKiemTras(baiKiemTrasData);
      // const cauHoisData = await getCauHoiByBaiKiemTraId(baiKiemTrasData[0].id);
      // console.log("cauHoisData: ", cauHoisData);
      // setCauHois(cauHoisData);
    };
    fetchData();
  }, [lopHocPhanId]);

  return (
    <div>
      Công Thức Điểm Component for Lớp Học Phần ID: {lopHocPhanId}
      <div className="flex">
        <div className="w-1/2 p-2">
          <DataTable
            columnToBeFiltered={"loai"}
            hasSelectedRowsCount={false}
            isPaginated={false}
            data={baiKiemTras}
            columns={baiKiemTraColumns}
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
                      const cauHoisData = await getCauHoiByBaiKiemTraId(
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
            columnToBeFiltered={"loai"}
            hasSelectedRowsCount={false}
            isPaginated={false}
            data={cauHois}
            columns={cauHoiColumns}
          />
        </div>
      </div>
    </div>
  );
};

export default CongThucDiem;
