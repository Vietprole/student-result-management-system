import { useEffect, useState, useCallback } from 'react';
import { useParams } from 'react-router-dom';
import { getPLOsByLopHocPhanId, getCLOsByPLOId, addCLOsToPLO } from '@/api/api-plo';
import { getCLOsByLopHocPhanId } from '@/api/api-clo';

const ToggleCell = ({ cloId, ploId, isEditable, table }) => {
  const [toggled, setToggled] = useState(false);

  useEffect(() => {
    const checkToggleStatus = async () => {
      try {
        const ploData = await getCLOsByPLOId(ploId);
        setToggled(ploData.some(clo => clo.id === cloId));
      } catch (error) {
        console.error("Error fetching CLOs by PLO ID:", error);
      }
    };
    checkToggleStatus();
  }, [cloId, ploId]);

  const onToggle = () => {
    if (isEditable) {
      setToggled(!toggled);
      table.options.meta?.updateData(cloId, ploId, !toggled);
    }
  };

  return (
    <div
      className={`p-2 ${toggled ? "bg-blue-500 text-white" : "bg-white text-black"} ${isEditable ? "cursor-pointer" : "cursor-not-allowed opacity-50"}`}
      onClick={onToggle}
    >
      {toggled ? "✓" : ""}
    </div>
  );
};

export default function PLOCLOTable() {
  const [cLOs, setCLOs] = useState([]);
  const [pLOs, setPLOs] = useState([]);
  const [isEditable, setIsEditable] = useState(false);
  const [toggledData, setToggledData] = useState({});
  const { lopHocPhanId } = useParams();

  const fetchData = useCallback(async () => {
    try {
      const [cLOsData, pLOsData] = await Promise.all([
        getCLOsByLopHocPhanId(lopHocPhanId),
        getPLOsByLopHocPhanId(lopHocPhanId),
      ]);
      setCLOs(cLOsData);
      setPLOs(pLOsData);

      const toggledData = {};
      for (const plo of pLOsData) {
        const cloData = await getCLOsByPLOId(plo.id);
        toggledData[plo.id] = cloData.map(clo => clo.id);
      }
      setToggledData(toggledData);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  }, [lopHocPhanId]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  console.log("Update the toggledData:", toggledData);

  const handleSaveChanges = async () => {
    try {
      for (const [ploId, cloIdsList] of Object.entries(toggledData)) {
        await addCLOsToPLO(ploId, cloIdsList);
      }
      setIsEditable(false);
      console.log("Changes saved successfully");
    } catch (error) {
      console.error("Error saving changes:", error);
    }
  };

  const updateData = (cloId, ploId, toggled) => {
    setToggledData(prev => {
      const updated = { ...prev };
      if (toggled) {
        if (!updated[ploId]) {
          updated[ploId] = [];
        }
        if (!updated[ploId].includes(cloId)) {
          updated[ploId].push(cloId);
        }
      } else {
        updated[ploId] = updated[ploId].filter(id => id !== cloId);
      }
      return updated;
    });
  };

  const columns = [
    {
      accessorKey: "ten",
      header: "CLO",
    },
    ...pLOs.map(plo => ({
      accessorKey: plo.id.toString(),
      header: plo.ten,
      cell: ({ row }) => (
        <ToggleCell
          cloId={row.original.id}
          ploId={plo.id}
          isEditable={isEditable}
          table={{ options: { meta: { updateData } } }}
        />
      ),
    })),
  ];

  return (
    <div>
      <button
        onClick={() => {
          if (isEditable) {
            handleSaveChanges();
          } else {
            setIsEditable(true);
          }
        }}
        className="mb-4 px-4 py-2 bg-blue-500 text-white rounded"
      >
        {isEditable ? "Save Changes" : "Enable Edit"}
      </button>
      <table className="min-w-full divide-y divide-gray-200">
        <thead className="bg-gray-50">
          <tr>
            {columns.map((column) => (
              <th key={column.accessorKey} className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                {column.header}
              </th>
            ))}
          </tr>
        </thead>
        <tbody className="bg-white divide-y divide-gray-200">
          {cLOs.map((clo) => (
            <tr key={clo.id}>
              {columns.map((column) => (
                <td key={column.accessorKey} className="px-6 py-4 whitespace-nowrap">
                  {column.cell ? column.cell({ row: { original: clo } }) : clo[column.accessorKey]}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
