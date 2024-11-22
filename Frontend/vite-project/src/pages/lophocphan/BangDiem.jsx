import { useParams } from 'react-router-dom';

const BangDiem = () => {
  const { lopHocPhanId } = useParams();
  return <div>Bảng Điểm Component for Lớp Học Phần ID: {lopHocPhanId}</div>;
};

export default BangDiem;
