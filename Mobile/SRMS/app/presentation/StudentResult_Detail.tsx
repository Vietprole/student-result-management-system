import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, FlatList, ScrollView} from 'react-native';
import * as math from 'mathjs';

type TableRow = {
  TT: string;
  MaMonHoc: string;
  MonHoc: string;
  SoTC: string;
  CongThucDiem: string;
  BT: string;
  CK: string;
  GK: string;
  QT: string;
  TongKet: string;
  Thang10: string;
  Thang4: string;
  DiemChu: string;
};

const calculateScores = (CK: string, GK: string, QT: string) => {
  if (!CK || !GK || !QT) return { TongKet: '', Thang10: '', Thang4: '', DiemChu: '' };

  const tongKet = math.evaluate(`${CK} * 0.5 + ${GK} * 0.2 + ${QT} * 0.3`);
  const thang4 = tongKet >= 8.5 ? 4.0 : tongKet >= 7.0 ? 3.0 : tongKet >= 5.5 ? 2.0 : tongKet >= 4.0 ? 1.0 : 0;
  const diemChu =
    tongKet >= 8.5
      ? 'A'
      : tongKet >= 7.0
        ? 'B'
        : tongKet >= 5.5
          ? 'C'
          : tongKet >= 4.0
            ? 'D'
            : 'F';

  return {
    TongKet: tongKet.toFixed(2),
    Thang10: tongKet.toFixed(2),
    Thang4: thang4.toFixed(1),
    DiemChu: diemChu,
  };
};

const App = () => {
  const tableHeaders = [
    'TT', 'Mã môn học', 'Môn học', 'Số tín chỉ', 'Công thức điểm',
    'BT', 'CK', 'GK', 'QT', 'Tổng kết', 'Thang 10', 'Thang 4', 'Điểm chữ'
  ];

  const tableData: TableRow[] = [
    {
      TT: '1', MaMonHoc: 'PBL6', MonHoc: 'PBL6', SoTC: '3', CongThucDiem: '50% CK + 20% GK + 30% QT',
      BT: '', CK: '9', GK: '9', QT: '8', TongKet: '', Thang10: '', Thang4: '', DiemChu: ''
    },
    {
      TT: '2', MaMonHoc: 'KTPM', MonHoc: 'Kiểm thử phần mềm', SoTC: '2', CongThucDiem: '50% CK + 20% GK + 30% QT',
      BT: '', CK: '7', GK: '8', QT: '6', TongKet: '', Thang10: '', Thang4: '', DiemChu: ''
    },
  ];

  const processedData = tableData.map(row => ({
    ...row,
    ...calculateScores(row.CK, row.GK, row.QT),
  }));

  const renderHeader = () => (
    <View style={[styles.row, styles.headerRow]}>
      {tableHeaders.map((header, index) => (
        <Text
          key={index}
          style={[
            styles.cell,
            index >= 5 ? styles.numericHeaderCell : styles.textHeaderCell, // Align numeric columns
          ]}
        >
          {header}
        </Text>
      ))}
    </View>
  );

  const renderRow = ({ item, index }: { item: TableRow; index: number }) => (
    <View style={[styles.row, index % 2 === 0 ? styles.evenRow : styles.oddRow]}>
      <Text style={styles.cell}>{item.TT}</Text>
      <Text style={styles.cell}>{item.MaMonHoc}</Text>
      <Text style={styles.cell}>{item.MonHoc}</Text>
      <Text style={styles.cell}>{item.SoTC}</Text>
      <Text style={styles.cell}>{item.CongThucDiem}</Text>
      <Text style={[styles.cell, styles.numericCell]}>{item.BT}</Text>
      <Text style={[styles.cell, styles.numericCell]}>{item.CK}</Text>
      <Text style={[styles.cell, styles.numericCell]}>{item.GK}</Text>
      <Text style={[styles.cell, styles.numericCell]}>{item.QT}</Text>
      <Text style={[styles.cell, styles.numericCell]}>{item.TongKet}</Text>
      <Text style={[styles.cell, styles.numericCell]}>{item.Thang10}</Text>
      <Text style={[styles.cell, styles.numericCell]}>{item.Thang4}</Text>
      <Text style={styles.cell}>{item.DiemChu}</Text>
    </View>
  );

  return (
    <ScrollView horizontal>
      <View style={styles.table}>
        {renderHeader()}
        <FlatList
          data={processedData}
          renderItem={renderRow}
          keyExtractor={(item, index) => index.toString()}
        />
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  table: {
    borderWidth: 1,
    borderColor: '#ccc',
    marginHorizontal: 16,
    paddingBottom: 16,
    flexWrap: 'wrap',
  },
  row: {
    flexDirection: 'row',
  },
  cell: {
    flex: 1,
    padding: 8,
    textAlign: 'center',
    borderWidth: 1,
    borderColor: '#ccc',
    minWidth: 100,
  },
  numericCell: {
    textAlign: 'center',
  },
  headerRow: {
    backgroundColor: '#f1f1f1',
  },
  textHeaderCell: {
    fontWeight: 'bold',
    fontSize: 14,
    textAlign: 'center',
  },
  numericHeaderCell: {
    fontWeight: 'bold',
    fontSize: 14,
    textAlign: 'center',
  },
  evenRow: {
    backgroundColor: '#f9f9f9',
  },
  oddRow: {
    backgroundColor: '#ffffff',
  },
});

export default App;



// const App = () => {
//   return (
//     <View style={styles.wrapper}>

//       {/* Table Container */}
//       <View style={styles.table}>
//         {/* Table Head */}
//         <View style={styles.table_head}>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>TT</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>Mã môn học</Text>
//           </View>
//           <View style={{ width: '45%' }}>
//             <Text style={styles.table_head_captions}>Môn học</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>Số tín chỉ</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>Công thức điểm</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>BT</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>CK</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>GK</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>TN</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>QT</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>Tổng kết</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>Thang 10</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>Thang 4</Text>
//           </View>
//           <View style={{ width: '45%' }}>
//             <Text style={styles.table_head_captions}>Điểm chữ</Text>
//           </View>
//         </View>

//         {/* Table Body - Single Row */}

//         <View style={styles.table_body_single_row}>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_data}>01</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_head_captions}>1023780.2410.21.11B	</Text>
//           </View>
//           <View style={{ width: '45%' }}>
//             <Text style={styles.table_data}>PBL6: dự án công nghệ phần mềm</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_data}>4</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_data}>50% CK + 20% GK + 30% QT</Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_data}></Text>
//           </View>
//           <View style={{ width: '15%' }}>
//             <Text style={styles.table_data}>9</Text>
//             <View style={{ width: '15%' }}>
//               <Text style={styles.table_data}></Text>
//             </View>
//             <View style={{ width: '15%' }}>
//               <Text style={styles.table_data}></Text>
//             </View>
//             <View style={{ width: '15%' }}>
//               <Text style={styles.table_data}>9</Text>
//             </View>
//             <View style={{ width: '15%' }}>
//               <Text style={styles.table_data}>calculateScores(row.CK, row.GK, row.QT)</Text>
//             </View>
//             <View style={{ width: '15%' }}>
//               <Text style={styles.table_data}>9</Text>
//             </View>
//           </View>
//         </View>

//         {/* Table Body - Single Row */}
        

//         {/* Table Body - Single Row */}
        

//         {/* Table Body - Single Row */}

//       </View>
//     </View>
//   );
// };

// export default App;

// const styles = StyleSheet.create({
//   wrapper: {
//     justifyContent: 'center',
//     alignItems: 'center',
//     flex: 1,
//   },
//   table_head: {
//     flexDirection: 'row',
//     borderBottomWidth: 1,
//     borderColor: '#ddd',
//     padding: 7,
//     backgroundColor: '#3bcd6b'
//   },
//   table_head_captions: {
//     fontSize: 15,
//     color: 'white'
//   },

//   table_body_single_row: {
//     backgroundColor: '#fff',
//     flexDirection: 'row',
//     borderBottomWidth: 1,
//     borderColor: '#ddd',
//     padding: 7,
//   },
//   table_data: {
//     fontSize: 11,
//   },
//   table: {
//     margin: 15,
//     justifyContent: 'center',
//     alignItems: 'center',
//     elevation: 1,
//     backgroundColor: '#fff',
//   },

// });