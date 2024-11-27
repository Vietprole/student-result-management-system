import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_KHOA = `${API_BASE_URL}/api/khoa`;

export const getAllKhoas = async () => {
  try {
    const response = await axios.get(API_KHOA);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to get a single student by ID
export const getKhoaById = async (studentId) => {
  try {
    const response = await axios.get(`${API_KHOA}/${studentId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to add a new student
export const addKhoa = async (studentData) => {
  try {
    const response = await axios.post(API_KHOA, studentData);
    console.log("add data:" , response.data);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to update an existing student
export const updateKhoa = async (studentId, updatedData) => {
  try {
    const response = await axios.put(`${API_KHOA}/${studentId}`, updatedData);
    console.log("update data:" , response.data);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to delete a student
export const deleteKhoa = async (studentId) => {
  try {
    const response = await axios.delete(`${API_KHOA}/${studentId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
