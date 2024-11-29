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

// Function to get a single student by ID
export const getSinhVienById = async (studentId) => {
  try {
    const response = await axios.get(`${API_SINHVIEN}/${studentId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to add a new student
export const addSinhVien = async (studentData) => {
  try {
    const response = await axios.post(API_SINHVIEN, studentData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to update an existing student
export const updateSinhVien = async (studentId, updatedData) => {
  try {
    const response = await axios.put(`${API_SINHVIEN}/${studentId}`, updatedData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to delete a student
export const deleteSinhVien = async (studentId) => {
  try {
    const response = await axios.delete(`${API_SINHVIEN}/${studentId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
