import { useState, useEffect } from "react";
// import { getStudents, getGradeComponents, getQuestions, getGrades } from "@/lib/api"
import { GradeTable } from "@/components/GradeTable";
// import { StudentGrades, GradeComponent, Question, Grade } from "@/types/grades"
import { getSinhViens } from "@/api/api-sinhvien";
import { useParams } from "react-router-dom";
import { getBaiKiemTraById, getBaiKiemTrasByLopHocPhanId } from "@/api/api-baikiemtra";
import { getAllKetQuas } from "@/api/api-ketqua";
import { getCauHoisByBaiKiemTraId } from "@/api/api-cauhoi";
import { ComboBox } from "@/components/ComboBox";
import { Button } from "@/components/ui/button";
// import { set } from "react-hook-form";
import { useSearchParams, useNavigate } from "react-router-dom";

export default function BangDiemGiangVienPage() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const baiKiemTraIdParam = searchParams.get("baiKiemTraId");
  const [tableData, setTableData] = useState([]);
  const [components, setComponents] = useState([]);
  const [questions, setQuestions] = useState([]);
  const { lopHocPhanId } = useParams();
  const [baiKiemTraItems, setBaiKiemTraItems] = useState([]);
  const [baiKiemTraId, setBaiKiemTraId] = useState(baiKiemTraIdParam);
  const [comboBoxBaiKiemTraId, setComboBoxBaiKiemTraId] = useState(baiKiemTraIdParam);
  useEffect(() => {
    const fetchData = async () => {
      // Fetch all required data
      const [students, component, allGrades] = await Promise.all([
        // getStudents(),
        getSinhViens(null, lopHocPhanId),
        // getGradeComponents(),
        getBaiKiemTraById(baiKiemTraId),
        // getGrades(),
        getAllKetQuas(),
      ]);
      // Map khoa items to be used in ComboBox
      const components = [component];
      setComponents(components);
      const baiKiemTraData = await getBaiKiemTrasByLopHocPhanId(lopHocPhanId);
      const mappedComboBoxItems = baiKiemTraData.map(baiKiemTra => ({ label: baiKiemTra.loai, value: baiKiemTra.id }));
      setBaiKiemTraItems(mappedComboBoxItems);

      // Fetch questions for each component
      const questionsPromises = components.map(async (component) => ({
        componentId: component.id,
        // questions: await getQuestions(component.id),
        questions: await getCauHoisByBaiKiemTraId(baiKiemTraId),
      }));

      const questionsData = await Promise.all(questionsPromises);
      // console.log("baiKiemTraId", baiKiemTraId);
      // const questionsData = await getCauHoisByBaiKiemTraId(baiKiemTraId);
      console.log("questionsData", questionsData);
      const questions = Object.fromEntries(
        questionsData.map(({ componentId, questions }) => [
          // componentId.toString(),
          componentId,
          questions,
        ])
      );
      setQuestions(questions);
      
      console.log("students: ", students);
      console.log("allGrades: ", allGrades);
      // Transform data into the required format
      const tableData = students.map((student) => ({
        ...student,
        // tt: index + 1, // Add tt (ordinal number) to each student
        grades: Object.fromEntries(
          components.map((component) => [
            component.loai,
            Object.fromEntries(
              (questions[component.id.toString()] || []).map((question) => {
                const grade = allGrades.find(
                  (g) =>
                    g.sinhVienId === student.id && g.cauHoiId === question.id
                );
                console.log("grade: ", grade);
                // return [question.id.toString(), grade?.diem || 0];
                return [question.id, grade?.diemTam || 0];
              })
            ),
          ])
        ),
      }));
      console.log("tableData 86: ", tableData);
      setTableData(tableData);
    };
    fetchData();
  }, [lopHocPhanId, baiKiemTraId]);

  const handleGoClick = () => {
    setBaiKiemTraId(comboBoxBaiKiemTraId);
    if (comboBoxBaiKiemTraId === "") {
      navigate(`.`);
      return;
    }
    navigate(`.?baiKiemTraId=${comboBoxBaiKiemTraId}`);
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6">Nhập điểm cho từng thành phần</h1>
      <div className="flex">
        <ComboBox items={baiKiemTraItems} setItemId={setComboBoxBaiKiemTraId} initialItemId={comboBoxBaiKiemTraId}/>
        <Button onClick={handleGoClick}>Go</Button>
      </div>
      {console.log("tableData, components, questions", tableData, components, questions)}
      {tableData.length > 0 && (
        <GradeTable
          data={tableData}
          components={components}
          questions={questions}
        />
      )}
    </div>
  );
}