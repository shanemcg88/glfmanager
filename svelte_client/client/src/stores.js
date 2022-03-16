import { readable, writable } from "svelte/store";

export const ROOT_URL = readable(import.meta.env.VITE_ROOTURL)

export const employeeList = writable([]);

export const clientList = writable([]);

export const jobsList = writable([]);