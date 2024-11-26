import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_BAIKIEMTRA = `${API_BASE_URL}/api/baikiemtra`;

export const getBaiKiemTrasByLopHocPhanId = async (lopHocPhanId) => {
  console.log("lopHocPhanId: ", lopHocPhanId);
  try {
    const response = await axios.get(`${API_BAIKIEMTRA}?lopHocPhanId=${lopHocPhanId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getAllBaiKiemTras = async () => {
  try {
    const response = await axios.get(API_BAIKIEMTRA);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};


// Function to get a single baikiemtra by ID
export const getBaiKiemTraById = async (baikiemtraId) => {
  try {
    const response = await axios.get(`${API_BAIKIEMTRA}/${baikiemtraId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to add a new baikiemtra
export const addBaiKiemTra = async (baikiemtraData) => {
  try {
    const response = await axios.post(API_BAIKIEMTRA, baikiemtraData);
    console.log("add data:" , response.data);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to update an existing baikiemtra
export const updateBaiKiemTra = async (baikiemtraId, updatedData) => {
  try {
    const response = await axios.put(`${API_BAIKIEMTRA}/${baikiemtraId}`, updatedData);
    console.log("update data:" , response.data);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to delete a baikiemtra
export const deleteBaiKiemTra = async (baikiemtraId) => {
  try {
    const response = await axios.delete(`${API_BAIKIEMTRA}/${baikiemtraId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
