import API_BASE_URL from "./base-url";
import axios from "axios";
import { getAccessToken } from "../utils/storage";
import { createSearchURL } from "../utils/string";

const API_KETQUA = `${API_BASE_URL}/api/ketqua`;

// export const getKetQuasByLopHocPhanId = async (lopHocPhanId) => {
//   try {
//     const response = await axios.get(`${API_KETQUA}?lopHocPhanId=${lopHocPhanId}`);
//     return response.data;
//   } catch (error) {
//     console.log("error message: ", error.message);
//   }
// };

export const getAllKetQuas = async () => {
  try {
    const response = await axios.get(API_KETQUA, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getKetQuas = async (baiKiemTraId) => {
  try {
    const paramsObj = { baiKiemTraId };
    const url = createSearchURL(API_KETQUA, paramsObj);
    console.log("url kq: ", url);

    const response = await axios.get(url, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

// Function to get a single ketqua by ID
export const getKetQuaById = async (ketquaId) => {
  try {
    const response = await axios.get(`${API_KETQUA}/${ketquaId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to add a new ketqua
export const addKetQua = async (ketquaData) => {
  try {
    const response = await axios.post(API_KETQUA, ketquaData, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to update an existing ketqua
export const updateKetQua = async (updatedData) => {
  try {
    const response = await axios.put(`${API_KETQUA}`, updatedData, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const upsertKetQua = async (ketquaData) => {
  try {
    const response = await axios.put(`${API_KETQUA}/upsert`, ketquaData, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

// Function to delete a ketqua
export const deleteKetQua = async (ketquaId) => {
  try {
    const response = await axios.delete(`${API_KETQUA}/${ketquaId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const confirmKetQua = async (confirmKetQuaDTO) => {
  try {
    const response = await axios.post(`${API_KETQUA}/confirm`, confirmKetQuaDTO, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const calculateDiemCLO = async (sinhVienId, CLOId) => {
  try {
    const response = await axios.get(`${API_KETQUA}/calculate-diem-clo?sinhVienId=${sinhVienId}&CLOId=${CLOId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const calculateDiemCLOMax = async (CLOId) => {
  try {
    const response = await axios.get(`${API_KETQUA}/calculate-diem-clo-max?CLOId=${CLOId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const calculateDiemPk = async (lopHocPhanId, sinhVienId, ploId) => {
  try {
    const response = await axios.get(`${API_KETQUA}/calculate-diem-pk?lopHocPhanId=${lopHocPhanId}&sinhVienId=${sinhVienId}&ploId=${ploId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const calculateDiemPLO = async (sinhVienId, ploId) => {
  try {
    const response = await axios.get(`${API_KETQUA}/calculate-diem-plo?sinhVienId=${sinhVienId}&ploId=${ploId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}
