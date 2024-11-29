import API_BASE_URL from "./base-url";
import axios from 'axios';
const API_GIANGVIEN = `${API_BASE_URL}/api/taikhoan`;
export const loginApi = async (tenDangNhap, matKhau) => {
  try {
    const response = await axios.post(`${API_GIANGVIEN}/login`, {
      tenDangNhap,
      matKhau
    });
    if (response.status === 200) {
      return response.data; // Assuming `response.data` contains the needed token or success info
    } else {
      return {
        success: false,
        message: 'Unexpected response status'
      };
    }
  } catch (error) {
    console.log("Error message: ", error.message);
    return {
      success: false,
      message: 'Có lỗi xảy ra, vui lòng thử lại!'
    };
  }
};
