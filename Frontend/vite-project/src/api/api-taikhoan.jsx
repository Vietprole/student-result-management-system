import API_BASE_URL from "./base-url";
import axios from 'axios';
import "@/utils/storage"
import { getAccessToken } from "@/utils/storage";
import { createSearchURL } from "@/utils/string";

const API_TAIKHOAN = `${API_BASE_URL}/api/taikhoan`;

export const loginApi = async (tenDangNhap, matKhau) => {
  try {
    const response = await axios.post(`${API_TAIKHOAN}/login`, {
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

export const getAllTaiKhoans = async () => {
  try {
    const response = await axios.get(API_TAIKHOAN, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const getTaiKhoans = async (chucVuId) => {
  try {
    const paramsObj = { chucVuId };
    const url = createSearchURL(API_TAIKHOAN, paramsObj);
    console.log("url: ", url);
    const response = await axios.get(url, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const addTaiKhoan = async (data) => {
  try {
    const response = await axios.post(`${API_TAIKHOAN}/createTaiKhoan`, data, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const updateTaiKhoan = async (id, data) => {
  try {
    const response = await axios.put(`${API_TAIKHOAN}/${id}`, data, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const deleteTaiKhoan = async (id) => {
  try {
    const response = await axios.delete(`${API_TAIKHOAN}/${id}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

