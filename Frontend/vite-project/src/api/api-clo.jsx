import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_CLO = `${API_BASE_URL}/api/clo`;

export const getCLOsByLopHocPhanId = async (lopHocPhanId) => {
  try {
    const response = await axios.get(`${API_CLO}?lopHocPhanId=${lopHocPhanId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getAllCLOs = async () => {
  try {
    const response = await axios.get(API_CLO);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const getCLOById = async (id) => {
  try {
    const response = await axios.get(`${API_CLO}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const addCLO = async (newData) => {
  try {
    const response = await axios.post(API_CLO, newData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const updateCLO = async (id, updatedData) => {
  try {
    const response = await axios.put(`${API_CLO}/${id}`, updatedData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const deleteCLO = async (id) => {
  try {
    const response = await axios.delete(`${API_CLO}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
