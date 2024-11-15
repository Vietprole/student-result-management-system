import { createBrowserRouter } from "react-router-dom";
import MainPage from "@/pages/MainPage";
import KhoaPage from "@/pages/KhoaPage";
import SinhVienPage from "@/pages/SinhVienPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
  },
  {
    path: "/khoa",
    element: <KhoaPage />,
  },
  {
    path: "sinhvien",
    element: <SinhVienPage />,
  }
]);

export default router;
