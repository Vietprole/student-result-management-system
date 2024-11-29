import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_CTDT = `${API_BASE_URL}/api/ctdt`;

export const getAllCTDT = async () => {
  try {
    const response = await axios.get(API_CTDT);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const addCTDT = async (newData) => {
  try {
    const response = await axios.post(API_CTDT, newData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const updateCTDT = async (id, updatedData) => {
  try {
    console.log("id & updatedData", id, updatedData);
    const response = await axios.put(`${API_CTDT}/${id}`, updatedData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const deleteCTDT = async (id) => {
  try {
    const response = await axios.delete(`${API_CTDT}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
