import { useEffect, useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import Layout from "./Layout";
import { getAllLopHocPhans } from "../api/api-lophocphan";
import { DropdownMenu, DropdownMenuTrigger, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { ChevronDown } from "lucide-react";
import { useParams } from 'react-router-dom';

export default function LopHocPhanPage() {
  const [lopHocPhans, setLopHocPhans] = useState([]);
  const [selectedLopHocPhan, setSelectedLopHocPhan] = useState(null);
  const [selectedItem, setSelectedItem] = useState(null);
  const navigate = useNavigate();
  const { lopHocPhanId, itemId } = useParams();

  useEffect(() => {
    const fetchLopHocPhans = async () => {
      const data = await getAllLopHocPhans();
      setLopHocPhans(data);

      // Set the selected values from URL parameters
      if (lopHocPhanId) {
        const selectedLopHocPhan = data.find(lhp => lhp.id === lopHocPhanId);
        setSelectedLopHocPhan(selectedLopHocPhan);
      }
      if (itemId) {
        setSelectedItem(itemId);
      }
    };

    fetchLopHocPhans();
  }, [lopHocPhanId, itemId]);

  const handleRoute = () => {
    if (selectedLopHocPhan && selectedItem) {
      navigate(`/lophocphan/${selectedLopHocPhan.id}/${selectedItem}`);
    }
  };

  return (
    <Layout>
      <h1>This is LopHocPhanPage</h1>
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
              <DropdownMenuItem onSelect={() => setSelectedItem("cong-thuc-diem")}>Công Thức Điểm</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("bang-diem")}>Bảng Điểm</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("tao-clo")}>Tạo CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("noi-plo-clo")}>Nối PLO - CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("noi-cau-hoi-clo")}>Nối Câu Hỏi - CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("diem-clo")}>Điểm CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("tong-ket-clo")}>Tổng Kết CLO</DropdownMenuItem>
              <DropdownMenuItem onSelect={() => setSelectedItem("bao-cao-clo")}>Báo Cáo CLO</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
        <div className="flex items-end">
          <Button onClick={handleRoute} disabled={!selectedItem}>
            Go
          </Button>
        </div>
      </div>
      <Outlet/>
    </Layout>
  );
}
