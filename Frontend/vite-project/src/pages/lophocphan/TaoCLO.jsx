import { useParams } from 'react-router-dom';

const TaoCLO = () => {
  const { lopHocPhanId } = useParams();
  return <div>Tạo CLO Component for Lớp Học Phần ID: {lopHocPhanId}</div>;
};

export default TaoCLO;
