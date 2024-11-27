import CLOGradeTable from '@/components/CLOGradeTable';
import { useParams } from 'react-router-dom';

const DiemCLO = () => {
  const { lopHocPhanId } = useParams();
  return (
    <>
      <div>Điểm CLO Component for Lớp Học Phần ID: {lopHocPhanId}</div>
      <CLOGradeTable/>
    </>
  )
};

export default DiemCLO;
