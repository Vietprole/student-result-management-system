// Types
interface LoginResponse {
  success: boolean;
  message?: string;
  token?: string;
  userData?: {
    mssv: string;
    hoTen: string;
    lop: string;
    khoa: string;
    nienKhoa: string;
  };
}

interface LoginCredentials {
  username: string;
  password: string;
}

interface ApiClient {
  basePath: string;
  headers: {
    'Content-Type': string;
  };
  login: (credentials: LoginCredentials) => Promise<LoginResponse>;
  getStudentInfo: (mssv: string) => Promise<any>;
  getStudentGrades: (mssv: string, semester: string) => Promise<any>;
  getTeachingProgram: (mssv: string) => Promise<any>;
}

// API Client
const configuration: ApiClient = {
  basePath: 'http://localhost:5095/api',
  headers: {
    'Content-Type': 'application/json'
  },
  login: async ({ username, password }: LoginCredentials): Promise<LoginResponse> => {
    try {
      const response = await fetch(`${configuration.basePath}/auth/login`, {
        method: 'POST',
        headers: configuration.headers,
        body: JSON.stringify({ username, password })
      });
      return await response.json();
    } catch (error) {
      throw new Error('Login failed');
    }
  },

  getStudentInfo: async (mssv: string) => {
    try {
      const response = await fetch(`${configuration.basePath}/students/${mssv}`, {
        method: 'GET',
        headers: configuration.headers
      });
      return await response.json();
    } catch (error) {
      throw new Error('Failed to fetch student info');
    }
  },

  getStudentGrades: async (mssv: string, semester: string) => {
    try {
      const response = await fetch(`${configuration.basePath}/students/${mssv}/grades?semester=${semester}`, {
        method: 'GET',
        headers: configuration.headers
      });
      return await response.json();
    } catch (error) {
      throw new Error('Failed to fetch grades');
    }
  },

  getTeachingProgram: async (mssv: string) => {
    try {
      const response = await fetch(`${configuration.basePath}/students/${mssv}/program`, {
        method: 'GET',
        headers: configuration.headers
      });
      return await response.json();
    } catch (error) {
      throw new Error('Failed to fetch teaching program');
    }
  }
};

export const apiClient = configuration;