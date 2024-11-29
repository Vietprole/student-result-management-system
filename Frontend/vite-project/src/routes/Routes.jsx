import { createBrowserRouter } from "react-router-dom";
import MainPage from "@/pages/MainPage";
import KhoaPage from "@/pages/KhoaPage";
import SinhVienPage from "@/pages/SinhVienPage";
import GiangVienPage from "@/pages/GiangVienPage";
import CTDTPage from "@/pages/CTDTPage";
import HocPhanPage from "@/pages/HocPhanPage";
import LopHocPhanPage from "@/pages/LopHocPhanPage";
import KetQuaPage from "@/pages/KetQuaPage";
import XetChuanDauRaPage from "@/pages/XetChuanDauRaPage";
import HoSoCaNhanPage from "@/pages/HoSoCaNhanPage";
import CaiDatPage from "@/pages/CaiDatPage";

import CongThucDiem from "@/pages/lophocphan/CongThucDiem";
import BangDiem from "@/pages/lophocphan/BangDiem";
import TaoCLO from "@/pages/lophocphan/TaoCLO";
import NoiCLOPLO from "@/pages/lophocphan/NoiCLOPLO";
import NoiCauHoiCLO from "@/pages/lophocphan/NoiCauHoiCLO";
import DiemCLO from "@/pages/lophocphan/DiemCLO";
import TongKetCLO from "@/pages/lophocphan/TongKetCLO";
import BaoCaoCLO from "@/pages/lophocphan/BaoCaoCLO";

const lophocphans = [
  {
    path: "/",
    element: <MainPage />,
  },
  {
    path: "/khoa",
    element: <KhoaPage />,
  },
  {
    path: "/giangvien",
    element: <GiangVienPage />,
  },
  {
    path: "/sinhvien",
    element: <SinhVienPage />,
  },
  {
    path: "/ctdt",
    element: <CTDTPage />,
  },
  {
    path: "/hocphan",
    element: <HocPhanPage />,
  },
  {
    path: "/lophocphan",
    element: <LopHocPhanPage />,
    children: [
      { path: ":lopHocPhanId/cong-thuc-diem", element: <CongThucDiem /> },
      { path: ":lopHocPhanId/bang-diem", element: <BangDiem /> },
      { path: ":lopHocPhanId/tao-clo", element: <TaoCLO /> },
      { path: ":lopHocPhanId/noi-plo-clo", element: <NoiCLOPLO /> },
      { path: ":lopHocPhanId/noi-cau-hoi-clo", element: <NoiCauHoiCLO /> },
      { path: ":lopHocPhanId/diem-clo", element: <DiemCLO /> },
      { path: ":lopHocPhanId/tong-ket-clo", element: <TongKetCLO /> },
      { path: ":lopHocPhanId/bao-cao-clo", element: <BaoCaoCLO /> },
    ],
  },
  {
    path: "/ketqua",
    element: <KetQuaPage />,
  },
  {
    path: "/xetchuandaura",
    element: <XetChuanDauRaPage />,
  },
  {
    path: "/hosocanhan",
    element: <HoSoCaNhanPage />,
  },
  {
    path: "/caidat",
    element: <CaiDatPage />,
  }
]

const router = createBrowserRouter(lophocphans, {future: {
  v7_fetcherPersist: true,
  v7_normalizeFormMethod: true,
  v7_partialHydration: true,
  v7_relativeSplatPath: true,
  v7_skipActionErrorRevalidation: true,
}});

export default router;
