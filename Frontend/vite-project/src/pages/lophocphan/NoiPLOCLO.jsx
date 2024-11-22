import { useParams } from 'react-router-dom';

const NoiPLOCLO = () => {
  const { lopHocPhanId } = useParams();
  return <div>Nối PLO-CLO Component for Lớp Học Phần ID: {lopHocPhanId}</div>;
};

export default NoiPLOCLO;
