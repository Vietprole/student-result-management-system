import React, { useState } from 'react';
import { Alert, FlatList, ScrollView, StyleSheet, Text, TouchableOpacity, View, SafeAreaView, Platform, StatusBar, Image } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';
import { SelectList } from 'react-native-dropdown-select-list';
import { apiClient } from '../api/apiClient';
import Header from '../common/Header';
import Footer from '../common/Footer';

const handleFooterPress = (route: string) => {
  if (route === 'home') {
    router.push('/(tabs)/Home');
  } else if (route === 'person') {
    router.push('/(tabs)/Profile');
  }
};

type Table1Row = {
  Ma_TenNganh: string;
  Ma_TenCTDT: string;
  SoHK: string;
  TongSoTC: string;
  SoTCBatBuoc: string;
  SoTCTuChon: string;
};

type Table2Row = {
  TT: string;
  HK: string;
  TenHocPhan: string;
  MaHocPhan: string;
  SoTC: string;
};

type ColumnWidths = {
  [key: string]: number;
};

const Teaching_Program = () => {
  const tableHeaders = ['Mã - Tên ngành', 'Mã - Tên chương trình đào tạo', 'Số học kỳ', 'Tổng số tín chỉ yêu cầu', 'Số tín chỉ bắt buộc', 'Số tín chỉ tự chọn'];
  const table2Headers = ['TT', 'HK', 'Tên học phần', 'Mã học phần', 'Số TC'];
  const tableData1: Table1Row[] = [
    { Ma_TenNganh: '7480201-	', Ma_TenCTDT: '1024014- Công nghệ Thông tin K2021CLC Đặc thù_CNPM', SoHK: '8', TongSoTC: '130', SoTCBatBuoc: '122', SoTCTuChon: '8' },
  ];

  const tableData2: Table2Row[] = [
    { TT: '1', HK: '1', TenHocPhan: 'Giải tích 1', MaHocPhan: '3190111', SoTC: '4' },
    { TT: '2', HK: '1', TenHocPhan: 'Đại số tuyến tính', MaHocPhan: '3190260', SoTC: '3' },
    { TT: '3', HK: '1', TenHocPhan: 'Kỹ thuật lập trình', MaHocPhan: '1022863', SoTC: '3' },
    { TT: '4', HK: '1', TenHocPhan: 'Anh văn A2.1 (CLC)	', MaHocPhan: '4130040', SoTC: '3' },
    { TT: '5', HK: '1', TenHocPhan: 'Nhập môn ngành	', MaHocPhan: '1022940', SoTC: '2' },
    { TT: '6', HK: '1', TenHocPhan: 'TH kỹ thuật lập trình	', MaHocPhan: '1023060', SoTC: '0' },
    { TT: '7', HK: '1', TenHocPhan: 'Triết học Mác - Lênin	', MaHocPhan: '2090150', SoTC: '3' },
  ];
  const table1ColumnWidths: ColumnWidths = {
    Ma_TenNganh: 100,
    Ma_TenCTDT: 250,
    SoHK: 80,
    TongSoTC: 80,
    SoTCBatBuoc: 100,
    SoTCTuChon: 100
  };

  const table2ColumnWidths: ColumnWidths = {
    TT: 50,
    HK: 50,
    TenHocPhan: 200,
    MaHocPhan: 100,
    SoTC: 80
  };

  const renderTable1Header = () => (
    <View style={[styles.row, styles.headerRow]}>
      {tableHeaders.map((header, index) => (
        <Text
          key={index}
          style={[
            styles.cell,
            styles.headerCell,
            { width: Object.values(table1ColumnWidths)[index] }
          ]}
        >
          {header}
        </Text>
      ))}
    </View>
  );

  const renderTable2Header = () => (
    <View style={[styles.row, styles.headerRow]}>
      {table2Headers.map((header, index) => (
        <Text
          key={index}
          style={[
            styles.cell,
            styles.headerCell,
            { width: Object.values(table2ColumnWidths)[index] }
          ]}
        >
          {header}
        </Text>
      ))}
    </View>
  );

  const renderRow1 = ({ item, index }: { item: Table1Row; index: number }) => (
    <View style={[styles.row, index % 2 === 0 ? styles.evenRow : styles.oddRow]}>
      <Text style={[styles.cellTT, { width: table1ColumnWidths.Ma_TenNganh }]}>{item.Ma_TenNganh}</Text>
      <Text style={[styles.cell, { width: table1ColumnWidths.Ma_TenCTDT }]}>{item.Ma_TenCTDT}</Text>
      <Text style={[styles.cell, { width: table1ColumnWidths.SoHK }]}>{item.SoHK}</Text>
      <Text style={[styles.cell, { width: table1ColumnWidths.TongSoTC }]}>{item.TongSoTC}</Text>
      <Text style={[styles.cell, { width: table1ColumnWidths.SoTCBatBuoc }]}>{item.SoTCBatBuoc}</Text>
      <Text style={[styles.cell, { width: table1ColumnWidths.SoTCTuChon }]}>{item.SoTCTuChon}</Text>
    </View>
  );

  const renderRow2 = ({ item, index }: { item: Table2Row; index: number }) => (
    <View style={[styles.row, index % 2 === 0 ? styles.evenRow : styles.oddRow]}>
      <Text style={[styles.cellTT, { width: table2ColumnWidths.TT }]}>{item.TT}</Text>
      <Text style={[styles.cellTT, { width: table2ColumnWidths.HK }]}>{item.HK}</Text>
      <Text style={[styles.cell, { width: table2ColumnWidths.TenHocPhan }]}>{item.TenHocPhan}</Text>
      <Text style={[styles.cell, { width: table2ColumnWidths.MaHocPhan }]}>{item.MaHocPhan}</Text>
      <Text style={[styles.cell, { width: table2ColumnWidths.SoTC }]}>{item.SoTC}</Text>
    </View>
  );

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#0000cc" />
      <View style={styles.container}>
        {/* Header */}
        <Header title="CHƯƠNG TRÌNH ĐÀO TẠO" />

        {/* Table Content */}
        <ScrollView>
          <Text style={styles.tableTitle}>Thông tin chương trình đào tạo</Text>
          <ScrollView horizontal>
            <View style={styles.table}>
              {renderTable1Header()}
              <FlatList
                data={tableData1}
                renderItem={renderRow1}
                keyExtractor={(item, index) => index.toString()}
                scrollEnabled={false}
              />
            </View>
          </ScrollView>

          <Text style={styles.tableTitle}>Danh sách học phần</Text>
          <ScrollView horizontal>
            <View style={styles.table}>
              {renderTable2Header()}
              <FlatList
                data={tableData2}
                renderItem={renderRow2}
                keyExtractor={(item, index) => index.toString()}
                scrollEnabled={false}
              />
            </View>
          </ScrollView>
        </ScrollView>

        {/* Footer */}
        <Footer />
      </View>
    </SafeAreaView>
  );
};

const footerItems: { icon: 'home' | 'person', label: string }[] = [
  { icon: 'home', label: 'Trang chủ' },
  { icon: 'person', label: 'Hồ sơ' },
];

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#000',
  },
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  headerRow: {
    backgroundColor: '#f1f1f1',
    borderBottomWidth: 2,
    borderBottomColor: '#ccc',
  },
  headerCell: {
    fontWeight: 'bold',
    fontSize: 14,
    backgroundColor: '#f1f1f1',
    textAlign: 'center',
  },
  dropdownContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    gap: 10,
  },
  dropdown: {
    flex: 1,
    borderColor: '#0000cc',
    borderRadius: 8,
    height: 50,
    paddingHorizontal: 12,
  },
  dropdownList: {
    borderColor: '#0000cc',
    marginTop: 5,
    maxHeight: 150,
  },
  selectButton: {
    backgroundColor: '#0000cc',
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
    height: 43,
  },
  selectButtonText: {
    color: 'white',
    fontWeight: 'bold',
    fontSize: 16,
  },
  tableTitle: {
    fontSize: 16,
    fontWeight: 'bold',
    marginHorizontal: 16,
    marginTop: 16,
    marginBottom: 8,
    color: '#0000cc',
  },
  table: {
    borderWidth: 1,
    borderColor: '#ccc',
    marginHorizontal: 16,
    borderRadius: 8,
    overflow: 'hidden',
    margin: 16,
    backgroundColor: 'white',
    marginTop: 8,
  },
  row: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
  },
  cellTT: {
    padding: 10,
    textAlign: 'center',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
    alignItems: 'center',
  },
  cell: {
    padding: 10,
    textAlign: 'left',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
    alignItems: 'center',
  },
  evenRow: {
    backgroundColor: '#f9f9f9',
  },
  oddRow: {
    backgroundColor: '#ffffff',
  },
  link: {
    color: 'blue',
    textDecorationLine: 'underline',
    textAlign: 'center',
  },
});

export default Teaching_Program;