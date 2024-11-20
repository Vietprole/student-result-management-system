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

const routes = [
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

const router = createBrowserRouter(routes, {future: {
  v7_fetcherPersist: true,
  v7_normalizeFormMethod: true,
  v7_partialHydration: true,
  v7_relativeSplatPath: true,
  v7_skipActionErrorRevalidation: true,
}});

export default router;
