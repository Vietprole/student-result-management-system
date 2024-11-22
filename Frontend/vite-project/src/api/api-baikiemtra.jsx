import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_BAIKIEMTRA = `${API_BASE_URL}/api/baikiemtra`;

export const getBaiKiemTraByLopHocPhanId = async (lopHocPhanId) => {
  console.log("lopHocPhanId: ", lopHocPhanId);
  try {
    const response = await axios.get(API_BAIKIEMTRA);
    console.log("response.data: ", response.data);
    const filteredData = response.data.filter(item => item.lopHocPhanId == lopHocPhanId);
    console.log("filteredData: ", filteredData);
    return filteredData;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
