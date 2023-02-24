import axios from "axios";
import { IStatus } from "../interfaces/IStatus";
import { ITask } from "../interfaces/ITask";

axios.defaults.baseURL = 'http://localhost:5160/api/';

// axios.interceptors.response.use(undefined, (error) => {
//   if (error.message === "Network Error" && !error.response) {
//     toast.error("Network error - make sure API is running!");
//   }
//   const { status, data, config, headers } = error.response;
//   if (status === 404) {
//     history.push("/notfound");
//   }
//   if (
//     status === 401 &&
//     headers["www-authenticate"] ===
//       'Bearer error="invalid_token", error_description="The token is expired"'
//   ) {
//     window.localStorage.removeItem("jwt");
//     history.push("/");
//     toast.info("Your session has expired, please login again");
//   }
//   if (
//     status === 400 &&
//     config.method === "get" &&
//     data.errors.hasOwnProperty("id")
//   ) {
//     history.push("/notfound");
//   }
//   if (status === 500) {
//     toast.error("Server error - check the terminal for more info!");
//   }
//   throw error.response;
// });

const responseBody = (response: any) => response.data;

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    del: (url: string) => axios.delete(url).then(responseBody),
};

const Tasks = {
    list: (): Promise<ITask[]> => axios.get("/tasks").then(responseBody),
    create: (task: ITask) => requests.post("/tasks", task),
    statuslist: (): Promise<IStatus[]> => axios.get("/tasks/status-list").then(responseBody),
    details: (id: string): Promise<ITask> => requests.get(`/tasks/${id}`),
    update: (task: ITask) => requests.put(`/tasks`, task),
    delete: (id: string) => requests.del(`/tasks/${id}`),
 };

export default {
    Tasks
};
