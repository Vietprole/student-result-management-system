import DataTable from "@/components/DataTable";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";
import { Button } from "@/components/ui/button";
import { getAllBaiKiemTras, getBaiKiemTrasByLopHocPhanId } from "@/api/api-baiKiemTra";
import {
  getCauHoisByBaiKiemTraId,
  // getAllCauHois,
  deleteCauHoi,
} from "@/api/api-cauhoi";
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
import { CauHoiForm } from "@/components/CauHoiForm";
import { useState, useEffect, useCallback } from "react";
import { ComboBox } from "@/components/ComboBox";
import { useSearchParams } from "react-router-dom";
import { useNavigate, useParams } from "react-router-dom";

export default function QuanLyCauHoi() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const { lopHocPhanId } = useParams();
  const baiKiemTraIdParam = searchParams.get("baiKiemTraId");
  const [data, setData] = useState([]);
  const [baiKiemTraItems, setBaiKiemTraItems] = useState([]);
  const [baiKiemTraId, setBaiKiemTraId] = useState(baiKiemTraIdParam);
  // const [lopHocPhanId, setLopHocPhanId] = useState(lopHocPhanIdParam);
  const [comboBoxBaiKiemTraId, setComboBoxBaiKiemTraId] = useState(baiKiemTraIdParam);

  const fetchData = useCallback(async () => {
    const dataBaiKiemTra = await getBaiKiemTrasByLopHocPhanId(lopHocPhanId);
    // Map baiKiemTra items to be used in ComboBox
    const mappedComboBoxItems = dataBaiKiemTra.map(baiKiemTra => ({ label: baiKiemTra.loai, value: baiKiemTra.id }));
    setBaiKiemTraItems(mappedComboBoxItems);
    const data = await getCauHoisByBaiKiemTraId(baiKiemTraId);
    setData(data);
  }, [baiKiemTraId, lopHocPhanId]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);


  const handleGoClick = () => {
    setBaiKiemTraId(comboBoxBaiKiemTraId);
    if (comboBoxBaiKiemTraId === "") {
      navigate(`.`);
      return;
    }
    navigate(`.?baiKiemTraId=${comboBoxBaiKiemTraId}`);
  };

  const createCauHoiColumns = (handleEdit, handleDelete) => [
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
            Trọng Số
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("trongSo")}</div>,
    },
    {
      accessorKey: "thangDiem",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Thang Điểm
            <ArrowUpDown />
          </Button>
        );
      },
      cell: ({ row }) => <div className="px-4 py-2">{row.getValue("thangDiem")}</div>,
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
                    Sửa Câu Hỏi
                  </DropdownMenuItem>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle>Edit CauHoi</DialogTitle>
                    <DialogDescription>
                      Edit the current item.
                    </DialogDescription>
                  </DialogHeader>
                  <CauHoiForm cauHoi={item} handleEdit={handleEdit} />
                </DialogContent>
              </Dialog>
              <Dialog>
                <DialogTrigger asChild>
                  <DropdownMenuItem onSelect={(e) => e.preventDefault()}>
                    Xóa Câu Hỏi
                  </DropdownMenuItem>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle>Delete CauHoi</DialogTitle>
                    <DialogDescription>
                      Delete the current item.
                    </DialogDescription>
                  </DialogHeader>
                  <p>Are you sure you want to delete this CauHoi?</p>
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
    <div className="w-full">
      <div className="flex">
        <ComboBox items={baiKiemTraItems} setItemId={setComboBoxBaiKiemTraId} initialItemId={comboBoxBaiKiemTraId}/>
        <Button onClick={handleGoClick}>Go</Button>
      </div>
      <DataTable
        entity="CauHoi"
        createColumns={createCauHoiColumns}
        data={data}
        setData={setData}
        fetchData={fetchData}
        deleteItem={deleteCauHoi}
        columnToBeFiltered={"ten"}
        ItemForm={CauHoiForm}
      />
    </div>
  );
}
