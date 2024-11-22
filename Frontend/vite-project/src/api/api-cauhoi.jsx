import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_CAUHOI = `${API_BASE_URL}/api/cauhoi`;

export const getCauHoiByBaiKiemTraId = async (baiKiemTraId) => {
  try {
    const response = await axios.get(API_CAUHOI);
    const filteredData = response.data.filter(item => item.baiKiemTraId == baiKiemTraId);
    return filteredData;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
