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

// Function to get a single sinhvien by ID
export const getSinhVienById = async (sinhvienId) => {
  try {
    const response = await axios.get(`${API_SINHVIEN}/${sinhvienId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to add a new sinhvien
export const addSinhVien = async (sinhvienData) => {
  try {
    const response = await axios.post(API_SINHVIEN, sinhvienData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to update an existing sinhvien
export const updateSinhVien = async (sinhvienId, updatedData) => {
  try {
    const response = await axios.put(`${API_SINHVIEN}/${sinhvienId}`, updatedData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to delete a sinhvien
export const deleteSinhVien = async (sinhvienId) => {
  try {
    const response = await axios.delete(`${API_SINHVIEN}/${sinhvienId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
