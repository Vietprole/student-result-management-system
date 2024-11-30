import API_BASE_URL from "./base-url";
import axios from 'axios';
import { getAccessToken } from "../utils/storage";

const API_HOCPHAN = `${API_BASE_URL}/api/hocphan`;

export const getAllHocPhans = async () => {
  try {
    const response = await axios.get(API_HOCPHAN, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to get a single hocphan by ID
export const getHocPhanById = async (hocphanId) => {
  try {
    const response = await axios.get(`${API_HOCPHAN}/${hocphanId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to add a new hocphan
export const addHocPhan = async (hocphanData) => {
  try {
    const response = await axios.post(API_HOCPHAN, hocphanData, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to update an existing hocphan
export const updateHocPhan = async (hocphanId, updatedData) => {
  try {
    const response = await axios.put(`${API_HOCPHAN}/${hocphanId}`, updatedData, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

// Function to delete a hocphan
export const deleteHocPhan = async (hocphanId) => {
  try {
    const response = await axios.delete(`${API_HOCPHAN}/${hocphanId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};
