import { useEffect, useState } from "react";
import { useOutlet, useNavigate } from "react-router-dom";
import Layout from "./Layout";
import { getAllLopHocPhans } from "../api/api-lophocphan";
import { DropdownMenu, DropdownMenuTrigger, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { ChevronDown } from "lucide-react";
import DefaultComponent from "./nhapdiem/DefaultComponent";

export default function NhapDiemPage() {
  const [lopHocPhans, setLopHocPhans] = useState([]);
  const [selectedLopHocPhan, setSelectedLopHocPhan] = useState(null);
  const [selectedItem, setSelectedItem] = useState(null);
  const navigate = useNavigate();
  const outlet = useOutlet();

  useEffect(() => {
    const fetchLopHocPhans = async () => {
      const data = await getAllLopHocPhans();
      setLopHocPhans(data);
    };

    fetchLopHocPhans();
  }, []);

  const handleRoute = () => {
    let selectedRoute = null;
    switch (selectedItem) {
      case "Quản lý Câu hỏi":
        selectedRoute = "quan-ly-cau-hoi";
      break;
      case "Bảng Điểm":
        selectedRoute = "bang-diem";
      break;
      case "Tạo CLO":
        selectedRoute = "tao-clo";
      break;
      case "Nối PLO - CLO":
        selectedRoute = "noi-plo-clo";
      break;
      case "Nối Câu Hỏi - CLO":
        selectedRoute = "noi-cau-hoi-clo";
      break;
      case "Điểm CLO":
        selectedRoute = "diem-clo";
      break;
      case "Điểm Pk":
        selectedRoute = "diem-pk";
      break;
      case "Tổng Kết CLO":
        selectedRoute = "tong-ket-clo";
      break;
      case "Báo Cáo CLO":
        selectedRoute = "bao-cao-clo";
      break;
    }
    if (selectedLopHocPhan && selectedItem) {
      navigate(`/nhapdiem/${selectedLopHocPhan.id}/${selectedRoute}`);
    }
  };

  return (
    <Layout>
      <h1>This is NhapDiemPage</h1>
      <div className="flex space-x-4">
        <div>
          <h2>Chọn lớp học phần: </h2>
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline">
                {selectedLopHocPhan ? selectedLopHocPhan.ten : "Select Lop Hoc Phan"}
                <ChevronDown />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent>
              {lopHocPhans.map((lopHocPhan) => (
                <DropdownMenuItem
                  key={lopHocPhan.id}
                  onSelect={() => setSelectedLopHocPhan(lopHocPhan)}
                >
                  {lopHocPhan.ten}
                </DropdownMenuItem>
              ))}
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
        <div>
          <h2>Chọn item: </h2>
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline">
                {selectedItem ? `${selectedItem}` : "Select Item"}
                <ChevronDown />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent>
              <DropdownMenuItem onSelect={() => setSelectedItem("Quản lý Câu hỏi")}>Quản lý Câu hỏi</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("Bảng Điểm")}>Bảng Điểm</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("Tạo CLO")}>Tạo CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("Nối PLO - CLO")}>Nối PLO - CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("Nối Câu Hỏi - CLO")}>Nối Câu Hỏi - CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("Điểm CLO")}>Điểm CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("Điểm Pk")}>Điểm Pk</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("Tổng Kết CLO")}>Tổng Kết CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("Báo Cáo CLO")}>Báo Cáo CLO</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
        <div className="flex items-end">
          <Button onClick={handleRoute} disabled={!selectedItem}>
            Go
          </Button>
        </div>
      </div>
      {outlet}
    </Layout>
  );
}
