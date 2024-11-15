import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_SINHVIEN = `${API_BASE_URL}/api/sinhvien`;

export const getAllSinhViens = async () => {
  try {
    const response = await axios.get(API_SINHVIEN);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
