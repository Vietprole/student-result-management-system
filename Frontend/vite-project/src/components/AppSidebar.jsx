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
    url: "/",
    icon: DangXuatIcon,
  }
]
const giangVienItem = [
  {
    title: "Lớp học phần",
    url: "/lophocphan",
    icon: LopHocPhanIcon,
  },
  {
    title: "Nhập điểm",
    url: "/nhapdiem",
    icon: LopHocPhanIcon,
  },
  {
    title: "Điểm Đính Chính",
    url: "/diemdinhchinh",
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

const sinhVienItem = [
  {
    title: "Kết quả học tập",
    url: "/ketqua",
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
    url: "/",
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
    subItems: [
      {
        title: "Quản lý sinh viên",
        url: "/hocphan/them",
      },
      {
        title: "Xem lớp học phần ",
        url:  "/lophocphan",
      },
    ],
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
    title: "Điểm Đính Chính",
    url: "/diemdinhchinh",
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
    title: "Quản lý tài khoản",
    url: "/quanlytaikhoan",
    icon: HoSoCaNhanIcon,
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
    url: "/",
    icon: DangXuatIcon,
  },

]
export function AppSidebar() {
  // const role = getRole(); // Hàm getRole() cần được định nghĩa để lấy vai trò người dùng
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
                                className={`flex items-center p-2 rounded-lg ${
                                  location.pathname === subItem.url
                                    ? "bg-blue-100 text-blue-600"
                                    : "hover:bg-gray-100"
                                }`}
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
