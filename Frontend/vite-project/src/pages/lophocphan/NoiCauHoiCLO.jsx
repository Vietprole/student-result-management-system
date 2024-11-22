import { useParams } from 'react-router-dom';

const NoiCauHoiCLO = () => {
  const { lopHocPhanId } = useParams();
  return <div>Nối Câu Hỏi CLO Component for Lớp Học Phần ID: {lopHocPhanId}</div>;
};

export default NoiCauHoiCLO;
