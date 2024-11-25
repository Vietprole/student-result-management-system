import DataTable from "@/components/DataTable";
import Layout from "./Layout";
import columns from "@/app/columns";
import { getAllGiangViens } from "@/api/api-giangvien";
import { useEffect, useState } from "react";

export default function GiangVienPage() {
  const [data, setData] = useState([]);
  useEffect(() => {
    const fetchData = async () => {
      const data = await getAllGiangViens();
      setData(data);
    };
    fetchData();
  }, []);
  return (
    <Layout>
      <h1>This is GiangVienPage</h1>
      {/* <DataTable 
        columns={columns} 
        data={data}
      /> */}
    </Layout>
  );
}
