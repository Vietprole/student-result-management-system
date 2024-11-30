import { useEffect, useState } from 'react';
// import { useParams } from 'react-router-dom';
// import { getlistColumnItemByLopHocPhanId, getCLOsBycolumnItemId, updateCLOsToPLO } from '@/api/api-plo';
// import { getCLOsByLopHocPhanId } from '@/api/api-clo';

const ToggleCell = ({ rowItemId, columnItemId, isEditable, table, getRowItemsByColumnItemId }) => {
  const [toggled, setToggled] = useState(false);

  useEffect(() => {
    const checkToggleStatus = async () => {
      try {
        // const ploData = await getCLOsBycolumnItemId(columnItemId);
        const ploData = await getRowItemsByColumnItemId(columnItemId);
        setToggled(ploData.some(clo => clo.id === rowItemId));
      } catch (error) {
        console.error("Error fetching CLOs by PLO ID:", error);
      }
    };
    checkToggleStatus();
  }, [rowItemId, columnItemId, getRowItemsByColumnItemId]);

  const onToggle = () => {
    if (isEditable) {
      setToggled(!toggled);
      table.options.meta?.updateData(rowItemId, columnItemId, !toggled);
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

export default function MappingTable({listRowItem, listColumnItem, extraHeaders, toggledDataFromParent, updateRowItemsToColumnItem, getRowItemsByColumnItemId}) {
  const [isEditable, setIsEditable] = useState(false);
  const [toggledData, setToggledData] = useState(toggledDataFromParent);

  const handleSaveChanges = async () => {
    try {
      for (const [columnItemId, rowItemIdList] of Object.entries(toggledData)) {
        await updateRowItemsToColumnItem(columnItemId, rowItemIdList);
      }
      setIsEditable(false);
      console.log("Changes saved successfully");
    } catch (error) {
      console.error("Error saving changes:", error);
    }
  };

  const updateData = (rowItemId, columnItemId, toggled) => {
    setToggledData(prev => {
      const updated = { ...prev };
      if (toggled) {
        if (!updated[columnItemId]) {
          updated[columnItemId] = [];
        }
        if (!updated[columnItemId].includes(rowItemId)) {
          updated[columnItemId].push(rowItemId);
        }
      } else {
        updated[columnItemId] = updated[columnItemId].filter(id => id !== rowItemId);
      }
      console.log("updated", updated);
      return updated;
    });
  };

  const columns = [
    {
      accessorKey: "ten",
      header: "CLO - PLO",
    },
    ...listColumnItem.map(columnItem => ({
      accessorKey: columnItem.id.toString(),
      header: columnItem.ten ?? columnItem.loai,
      cell: ({ row }) => (
        <ToggleCell
          rowItemId={row.original.id}
          columnItemId={columnItem.id}
          isEditable={isEditable}
          table={{ options: { meta: { updateData } } }}
          getRowItemsByColumnItemId={getRowItemsByColumnItemId}
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
          {extraHeaders && (
            <tr>
              <th 
                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider border"
              >
              </th>
              {Object.entries(extraHeaders).map(([key, value]) => (
                <th 
                  key={key} 
                  colSpan={value.colSpan} 
                  className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider border"
                >
                  {value.header}
                </th>
              ))}
            </tr>
          )}
          <tr>
            {columns.map((column) => (
              <th key={column.accessorKey} className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider w-1/5 border">
                {column.header}
              </th>
            ))}
          </tr>
        </thead>
        <tbody className="bg-white divide-y divide-gray-200">
          {listRowItem.map((rowItem) => (
            <tr key={rowItem.id}>
              {columns.map((column) => (
                <td key={column.accessorKey} className="px-6 py-4 whitespace-nowrap w-1/5 border">
                  {column.cell ? column.cell({ row: { original: rowItem } }) : rowItem[column.accessorKey]}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}