import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_GIANGVIEN = `${API_BASE_URL}/api/giangvien`;

export const getAllGiangViens = async () => {
  try {
    const response = await axios.get(API_GIANGVIEN);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getGiangVienById = async (id) => {
  try {
    const response = await axios.get(`${API_GIANGVIEN}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const addGiangVien = async (newData) => {
  try {
    const response = await axios.post(API_GIANGVIEN, newData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};


export const updateGiangVien = async (id, updatedData) => {
  try {
    const response = await axios.put(`${API_GIANGVIEN}/${id}`, updatedData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const deleteGiangVien = async (id) => {
  try {
    const response = await axios.delete(`${API_GIANGVIEN}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
