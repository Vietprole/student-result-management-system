import API_BASE_URL from "./base-url";
import axios from "axios";
import { getAccessToken } from "../utils/storage";

const API_DIEMDINHCHINH = `${API_BASE_URL}/api/diemdinhchinh`;

export const upsertDiemDinhChinh = async (diemDinhChinhData) => {
  try {
    const response = await axios.put(`${API_DIEMDINHCHINH}/upsert`, diemDinhChinhData, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}
