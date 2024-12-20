import { useState, useEffect } from "react";
// import { getStudents, getGradeComponents, getQuestions, getGrades } from "@/lib/api"
import { GradeTable } from "@/components/GradeTable";
// import { StudentGrades, GradeComponent, Question, Grade } from "@/types/grades"
import { getSinhViens } from "@/api/api-sinhvien";
import { useParams } from "react-router-dom";
import { getBaiKiemTrasByLopHocPhanId } from "@/api/api-baikiemtra";
import { getAllKetQuas } from "@/api/api-ketqua";
import { getCauHoisByBaiKiemTraId } from "@/api/api-cauhoi";
// import { set } from "react-hook-form";

export default function GradesPage() {
  const [tableData, setTableData] = useState([]);
  const [components, setComponents] = useState([]);
  const [questions, setQuestions] = useState([]);
  const { lopHocPhanId } = useParams();
  useEffect(() => {
    const fetchData = async () => {
      // Fetch all required data
      const [students, components, allGrades] = await Promise.all([
        // getStudents(),
        getSinhViens(null, lopHocPhanId),
        // getGradeComponents(),
        getBaiKiemTrasByLopHocPhanId(lopHocPhanId),
        // getGrades(),
        getAllKetQuas(),
      ]);
      setComponents(components);

      // Fetch questions for each component
      const questionsPromises = components.map(async (component) => ({
        componentId: component.id,
        // questions: await getQuestions(component.id),
        questions: await getCauHoisByBaiKiemTraId(component.id),
      }));
      const questionsData = await Promise.all(questionsPromises);
      const questions = Object.fromEntries(
        questionsData.map(({ componentId, questions }) => [
          // componentId.toString(),
          componentId,
          questions,
        ])
      );
      setQuestions(questions);

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
                // return [question.id.toString(), grade?.diem || 0];
                return [question.id, grade?.diemTam || 0];
              })
            ),
          ])
        ),
      }));
      setTableData(tableData);
    };
    fetchData();
  }, [lopHocPhanId]);

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6">Bảng điểm học phần</h1>
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
