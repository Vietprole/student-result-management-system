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

export const getAllCauHois = async () => {
  try {
    const response = await axios.get(API_CAUHOI);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to get a single student by ID
export const getCauHoiById = async (studentId) => {
  try {
    const response = await axios.get(`${API_CAUHOI}/${studentId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to add a new student
export const addCauHoi = async (studentData) => {
  try {
    const response = await axios.post(API_CAUHOI, studentData);
    console.log("add data:" , response.data);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to update an existing student
export const updateCauHoi = async (studentId, updatedData) => {
  try {
    const response = await axios.put(`${API_CAUHOI}/${studentId}`, updatedData);
    console.log("update data:" , response.data);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to delete a student
export const deleteCauHoi = async (studentId) => {
  try {
    const response = await axios.delete(`${API_CAUHOI}/${studentId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
