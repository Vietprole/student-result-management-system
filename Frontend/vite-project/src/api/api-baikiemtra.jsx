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
  }
  catch (error) {
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


// Function to get a single student by ID
export const getBaiKiemTraById = async (studentId) => {
  try {
    const response = await axios.get(`${API_BAIKIEMTRA}/${studentId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to add a new student
export const addBaiKiemTra = async (studentData) => {
  try {
    const response = await axios.post(API_BAIKIEMTRA, studentData);
    console.log("add data:" , response.data);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to update an existing student
export const updateBaiKiemTra = async (studentId, updatedData) => {
  try {
    const response = await axios.put(`${API_BAIKIEMTRA}/${studentId}`, updatedData);
    console.log("update data:" , response.data);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to delete a student
export const deleteBaiKiemTra = async (studentId) => {
  try {
    const response = await axios.delete(`${API_BAIKIEMTRA}/${studentId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
