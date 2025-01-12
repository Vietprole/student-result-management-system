interface LoginResponse {
  success: boolean;
  message?: string;
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
}

// API Client implementation
const configuration: ApiClient = {
  basePath: 'http://localhost:5095/swagger/v1/swagger.json',
  headers: {
    'Content-Type': 'application/json'
  },
  login: async ({ username, password }: LoginCredentials): Promise<LoginResponse> => {
    // Placeholder for actual API call
    return { success: true, message: 'Login successful' };
  }
};

export const apiClient = configuration;