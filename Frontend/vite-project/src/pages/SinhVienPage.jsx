import { DataTable } from "@/components/DataTable";
import Layout from "./Layout";
// import data from "@/app/default-data";
import columns from "@/app/columns";
import { getAllSinhViens } from "@/api/api-sinhvien";
import { useEffect, useState } from "react";

export default function SinhVienPage() {
  const [data, setData] = useState([]);
  useEffect(() => {
    const fetchData = async () => {
      const data = await getAllSinhViens();
      setData(data);
    };
    fetchData();
  }, []);
  return (
    <Layout>
      <h1>This is SinhVienPage</h1>
      <DataTable 
        columns={columns} 
        data={data}
      />
    </Layout>
  );
}
