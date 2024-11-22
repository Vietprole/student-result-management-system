import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_LOPHOCPHAN = `${API_BASE_URL}/api/lophocphan`;

export const getAllLopHocPhans = async () => {
  try {
    const response = await axios.get(API_LOPHOCPHAN);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
