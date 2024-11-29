import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_CAUHOI = `${API_BASE_URL}/api/cauhoi`;

export const getCauHoisByBaiKiemTraId = async (baiKiemTraId) => {
  try {
    const response = await axios.get(`${API_CAUHOI}?baiKiemTraId=${baiKiemTraId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getCauHoiById = async (id) => {
  try {
    const response = await axios.get(`${API_CAUHOI}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const addCauHoi = async (newData) => {
  try {
    const response = await axios.post(API_CAUHOI, newData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const updateCauHoi = async (id, updatedData) => {
  try {
    const response = await axios.put(`${API_CAUHOI}/${id}`, updatedData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const deleteCauHoi = async (id) => {
  try {
    const response = await axios.delete(`${API_CAUHOI}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getCLOsByCauHoiId = async (cauHoiId) => {
  try {
    const response = await axios.get(`${API_CAUHOI}/${cauHoiId}/view-clos`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const addCLOsToCauHoi = async (cauHoiId, cloIdsList) => {
  try {
    const response = await axios.post(`${API_CAUHOI}/${cauHoiId}/add-clos`, cloIdsList);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const updateCLOsToCauHoi = async (cauHoiId, cloIdsList) => {
  try {
    const response = await axios.put(`${API_CAUHOI}/${cauHoiId}/update-clos`, cloIdsList);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const removeCLOsFromCauHoi = async (cauHoiId, cloId) => {
  try {
    const response = await axios.delete(`${API_CAUHOI}/${cauHoiId}/remove-clo/${cloId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}
