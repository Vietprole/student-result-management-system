import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupContent,
  // SidebarGroupLabel,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubItem,
  SidebarMenuSubButton,
} from "@/components/ui/sidebar"
import React, { useState, useEffect } from "react";
import "@/utils/storage"
import { Collapsible, CollapsibleTrigger, CollapsibleContent } from "@/components/ui/collapsible";
import { FiChevronDown, FiChevronUp } from 'react-icons/fi';
import { useLocation } from "react-router-dom";

// Import PNG icons
import KhoaIcon from "@/assets/icons/khoa-icon.png";
import GiangVienIcon from "@/assets/icons/giang-vien-icon.png";
import SinhVienIcon from "@/assets/icons/sinh-vien-icon.png";
import HocPhanIcon from "@/assets/icons/hoc-phan-icon.png";
import LopHocPhanIcon from "@/assets/icons/lop-hoc-phan-icon.png";
import KetQuaIcon from "@/assets/icons/ket-qua-hoc-tap-icon.png";
import XetChuanDauRaIcon from "@/assets/icons/xet-chuan-dau-ra-icon.png";
import HoSoCaNhanIcon from "@/assets/icons/ho-so-ca-nhan-icon.png";
import CaiDatIcon from "@/assets/icons/cai-dat-icon.png";
import DangXuatIcon from "@/assets/icons/dang-xuat-icon.png";
import LogoDUT from "@/assets/logos/logo-dut.png";
import { getRole } from "@/utils/storage";
const truongKhoaItem = [
  {
    title: "Khoa",
    url: "/khoa",
    icon: KhoaIcon,
  },
  {
    title: "Sinh viên",
    url: "/sinhvien",
    icon: SinhVienIcon,
  },
  {
    title: "Hồ sơ cá nhân",
    url: "/hosocanhan",
    icon: HoSoCaNhanIcon,
  },
  {
    title: "Cài đặt",
    url: "/caidat",
    icon: CaiDatIcon,
  },
  {
    title: "Đăng xuất",
    url: "/dangxuat",
    icon: DangXuatIcon,
  }
]
const giangvienitem = [
  {
    title: "L���p học phần",
    url: "/lophocphan",
    icon: LopHocPhanIcon,
  },
  {
    title: "Kết quả học tập",
    url: "/ketqua",
    icon: KetQuaIcon,
  },
  {
    title: "Xét chuẩn đầu ra",
    url: "/xetchuandaura",
    icon: XetChuanDauRaIcon,
  },
  {
    title: "Hồ sơ cá nhân",
    url: "/hosocanhan",
    icon: HoSoCaNhanIcon,
  },
  {
    title: "Cài đặt",
    url: "/caidat",
    icon: CaiDatIcon,
  },
  {
    title: "Đăng xuất",
    url: "/dangxuat",
    icon: DangXuatIcon,
  },
]

const sinhVienItem = [
  {
    title: "Kết quả học tập",
    url: "/ketquahoctap",
    icon: KetQuaIcon,
  },
  {
    title: "Hồ sơ cá nhân",
    url: "/hosocanhan",
    icon: HoSoCaNhanIcon,
  },
  {
    title: "Cài đặt",
    url: "/caidat",
    icon: CaiDatIcon,
  },
  {
    title: "Đăng xuất",
    url: "/dangxuat",
    icon: DangXuatIcon,
  }
]
// Menu items.
const adminItem = [
  {
    title: "Khoa",
    url: "/khoa",
    icon: KhoaIcon,
  },
  {
    title: "Ngành",
    url: "/nganh",
    icon: KhoaIcon, //TODO Change to Nganh icon
  },
  {
    title: "Học phần",
    url: "/hocphan",
    icon: HocPhanIcon,
    subItems: [
      {
        title: "Thêm Học Phần vào Ngành",
        url: "/hocphan/them",
      },
      {
        title: "Xem Học Phần",
        url: "/hocphan/",
      },
    ],
  },
  {
    title: "PLO",
    url: "/plo",
    icon: HocPhanIcon,
  },
  {
    title: "Nối Học Phần - PLO",
    url: "/maphocphanplo",
    icon: HocPhanIcon,
  },
  {
    title: "Giảng viên",
    url: "/giangvien",
    icon: GiangVienIcon,
  },
  {
    title: "Sinh viên",
    url: "/sinhvien",
    icon: SinhVienIcon,
  },
  {
    title: "Lớp học phần",
    url: "/lophocphan",
    icon: LopHocPhanIcon,
  },
  {
    title: "Công thức điểm",
    url: "/congthucdiem",
    icon: LopHocPhanIcon,
  },
  {
    title: "Nhập điểm",
    url: "/nhapdiem",
    icon: LopHocPhanIcon,
  },
  {
    title: "Kết quả học tập",
    url: "/ketqua",
    icon: KetQuaIcon,
  },
  {
    title: "Xét chuẩn đầu ra",
    url: "/xetchuandaura",
    icon: XetChuanDauRaIcon,
  },
  {
    title: "Hồ sơ cá nhân",
    url: "/hosocanhan",
    icon: HoSoCaNhanIcon,
  },
  {
    title: "Cài đặt",
    url: "/caidat",
    icon: CaiDatIcon,
  },
  {
    title: "Đăng xuất",
    url: "/",
    icon: DangXuatIcon,
  },
]
const phongDaoTaoItem = [
  {
    title: "Lớp học phần",
    url: "/lophocphan",
    icon: LopHocPhanIcon,
  },
  {
    title: "Kết quả học tập",
    url: "/ketqua",
    icon: KetQuaIcon,
  },
  {
    title: "PLO-CLO",
    url: "/xetchuandaura",
    icon: XetChuanDauRaIcon,
  },
  {
    title: "Hồ sơ cá nhân",
    url: "/hosocanhan",
    icon: HoSoCaNhanIcon,
  },
  {
    title: "Cài đặt",
    url: "/caidat",
    icon: CaiDatIcon,
  },
  {
    title: "Đăng xuất",
    url: "/dangxuat",
    icon: DangXuatIcon,
  },

]
export function AppSidebar() {
  const location = useLocation();
  const [openItem, setOpenItem] = useState(null);

  useEffect(() => {
    // Mở submenu nếu URL hiện tại khớp với một trong các subItems
    items.forEach(item => {
      if (item.subItems) {
        item.subItems.forEach(subItem => {
          if (location.pathname.startsWith(subItem.url)) {
            setOpenItem(item.title);
          }
        });
      }
    });
  }, [location.pathname]);

  let items = [];

  // switch (role) {
  //   case 'TruongKhoa':
  //     items = truongKhoaItem;
  //     break;
  //   case 'GiangVien':
  //     items = giangvienitem;
  //     break;
  //   case 'SinhVien':
  //     items = sinhVienItem;
  //     break;
  //   case 'Admin':
  //     items = adminItem;
  //     break;
  //   case 'PhongDaoTao':
  //     items = phongDaoTaoItem;
  //     break;
  //   default:
  //     console.warn('Vai trò không hợp lệ hoặc chưa được xác định.');
  //     break;
  // }
  items = adminItem;

  const toggleItem = (title, event) => {
    if (event) event.preventDefault();
    setOpenItem(openItem === title ? null : title);
  };

  const handleSubItemClick = (event) => {
    event.stopPropagation(); // Ngăn chặn sự kiện click lan ra ngoài
  };

  return (
    <Sidebar>
      <SidebarHeader>
        <a href="/khoa">
          <div className="flex items-center">
            <img src={LogoDUT} alt="Logo DUT" className="w-20 h-20 mr-2" />
            <span className="font-extrabold text-3xl text-blue-500">SRMS</span>
          </div>
        </a>
      </SidebarHeader>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupContent>
            <SidebarMenu>
              {items.map((item) => (
                <Collapsible key={item.title} open={openItem === item.title} className="group/collapsible">
                  <SidebarMenuItem>
                    <CollapsibleTrigger asChild>
                      <SidebarMenuButton asChild>
                        <a
                          href={item.url}
                          onClick={(e) => item.subItems && toggleItem(item.title, e)}
                          className={`flex items-center p-2 rounded-lg ${
                            window.location.pathname === item.url
                              ? "bg-blue-100 text-blue-600"
                              : "hover:bg-gray-100"
                          }`}
                        >
                          <img
                            src={item.icon}
                            alt={`${item.title} icon`}
                            className="w-6 h-6 mr-2"
                          />
                          <span>{item.title}</span>
                          {item.subItems && (
                            <span className="ml-auto">
                              {openItem === item.title ? <FiChevronUp /> : <FiChevronDown />}
                            </span>
                          )}
                        </a>
                      </SidebarMenuButton>
                    </CollapsibleTrigger>
                    {item.subItems && (
                      <CollapsibleContent>
                        <SidebarMenuSub>
                          {item.subItems.map((subItem) => (
                            <SidebarMenuSubItem key={subItem.title}>
                              <SidebarMenuSubButton
                                href={subItem.url}
                                onClick={handleSubItemClick} // Ngăn chặn sự kiện click lan ra ngoài
                              >
                                {subItem.title}
                              </SidebarMenuSubButton>
                            </SidebarMenuSubItem>
                          ))}
                        </SidebarMenuSub>
                      </CollapsibleContent>
                    )}
                  </SidebarMenuItem>
                </Collapsible>
              ))}
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
    </Sidebar>
  );
}
