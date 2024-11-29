import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_CHUONGTRINHDAOTAO = `${API_BASE_URL}/api/chuongtrinhdaotao`;

export const getAllChuongTrinhDaoTao = async () => {
  try {
    const response = await axios.get(API_CHUONGTRINHDAOTAO);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const addChuongTrinhDaoTao = async (newData) => {
  try {
    const response = await axios.post(API_CHUONGTRINHDAOTAO, newData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const updateChuongTrinhDaoTao = async (id, updatedData) => {
  try {
    const response = await axios.put(`${API_CHUONGTRINHDAOTAO}/${id}`, updatedData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const deleteChuongTrinhDaoTao = async (id) => {
  try {
    const response = await axios.delete(`${API_CHUONGTRINHDAOTAO}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};